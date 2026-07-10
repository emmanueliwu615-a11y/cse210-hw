using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

/// <summary>
/// Holds the full collection of Entry objects for the current
/// session and knows how to display, save, and load them.
/// Supports three file formats to exceed the core requirement:
/// .txt -> simple "~|~" separated lines (the required simplification)
/// .csv -> proper CSV with quote/comma escaping (Excel-friendly)
/// .json -> structured JSON via System.Text.Json
/// The format is chosen automatically from the file extension the
/// user types in, so the same Save/Load menu options handle all three.
/// </summary>
public class Journal
{
    private List<Entry> _entries;
    private const string Separator = "~|~";

    public Journal()
    {
        _entries = new List<Entry>();
    }

    public int Count => _entries.Count;

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    /// <summary>
    /// Prints every entry in the journal to the screen, plus a short
    /// summary (total entries and total words written) as an extra touch.
    /// </summary>
    public void Display()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("Your journal is empty. Write an entry first!");
            return;
        }

        Console.WriteLine();
        foreach (Entry entry in _entries)
        {
            Console.WriteLine(entry);
        }

        int totalWords = _entries.Sum(e => e.WordCount);
        Console.WriteLine($"Total entries: {_entries.Count} Total words written: {totalWords}");
    }

    public void SaveToFile(string filename)
    {
        string extension = Path.GetExtension(filename).ToLowerInvariant();

        switch (extension)
        {
            case ".csv":
                SaveAsCsv(filename);
                break;
            case ".json":
                SaveAsJson(filename);
                break;
            default:
                SaveAsPlainText(filename);
                break;
        }

        Console.WriteLine($"Journal saved to {filename} ({_entries.Count} entries).");
    }

    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine($"File not found: {filename}");
            return;
        }

        string extension = Path.GetExtension(filename).ToLowerInvariant();
        List<Entry> loaded;

        switch (extension)
        {
            case ".csv":
                loaded = LoadFromCsv(filename);
                break;
            case ".json":
                loaded = LoadFromJson(filename);
                break;
            default:
                loaded = LoadFromPlainText(filename);
                break;
        }

        _entries = loaded; // Replace current entries, per the spec.
        Console.WriteLine($"Journal loaded from {filename} ({_entries.Count} entries).");
    }

    // ---------- Plain text (~|~ separated) ----------

    private void SaveAsPlainText(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                writer.WriteLine(entry.ToFileLine(Separator));
            }
        }
    }

    private List<Entry> LoadFromPlainText(string filename)
    {
        List<Entry> result = new List<Entry>();
        foreach (string line in File.ReadAllLines(filename))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            result.Add(Entry.FromFileLine(line, Separator));
        }
        return result;
    }

    // ---------- CSV ----------

    private void SaveAsCsv(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine("Date,Prompt,Response,Mood");
            foreach (Entry entry in _entries)
            {
                writer.WriteLine(entry.ToCsvRow());
            }
        }
    }

    private List<Entry> LoadFromCsv(string filename)
    {
        List<Entry> result = new List<Entry>();
        List<string> lines = File.ReadAllLines(filename).ToList();
        if (lines.Count > 0) lines.RemoveAt(0); // skip header

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            List<string> fields = ParseCsvLine(line);
            string mood = fields.Count >= 4 ? fields[3] : "N/A";
            result.Add(new Entry(fields[0], fields[1], fields[2], mood));
        }
        return result;
    }

    // A small hand-rolled CSV parser that understands quoted fields
    // containing commas, so it correctly round-trips what ToCsvRow wrote.
    private List<string> ParseCsvLine(string line)
    {
        List<string> fields = new List<string>();
        StringBuilder current = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (inQuotes)
            {
                if (c == '"' && i + 1 < line.Length && line[i + 1] == '"')
                {
                    current.Append('"');
                    i++;
                }
                else if (c == '"')
                {
                    inQuotes = false;
                }
                else
                {
                    current.Append(c);
                }
            }
            else
            {
                if (c == '"')
                {
                    inQuotes = true;
                }
                else if (c == ',')
                {
                    fields.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }
        }
        fields.Add(current.ToString());
        return fields;
    }

    // ---------- JSON ----------

    private class EntryData
    {
        public string Date { get; set; }
        public string PromptText { get; set; }
        public string Response { get; set; }
        public string Mood { get; set; }
    }

    private void SaveAsJson(string filename)
    {
        List<EntryData> data = _entries.Select(e => new EntryData
        {
            Date = e.Date,
            PromptText = e.PromptText,
            Response = e.Response,
            Mood = e.Mood
        }).ToList();

        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(filename, json);
    }

    private List<Entry> LoadFromJson(string filename)
    {
        string json = File.ReadAllText(filename);
        List<EntryData> data = JsonSerializer.Deserialize<List<EntryData>>(json) ?? new List<EntryData>();
        return data.Select(d => new Entry(d.Date, d.PromptText, d.Response, d.Mood)).ToList();
    }
}


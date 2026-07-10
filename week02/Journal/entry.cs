using System;

/// <summary>
/// Represents a single journal entry: the prompt that was answered,
/// the user's response, the date it was written, and (as an extra
/// feature beyond the core requirements) an optional mood tag and
/// a computed word count.
/// </summary>
public class Entry
{
    // Member variables (fields) are kept private to demonstrate
    // abstraction -- outside code interacts with an Entry only
    // through its properties and methods, not its raw data.
    private string _date;
    private string _promptText;
    private string _response;
    private string _mood;

    public string Date => _date;
    public string PromptText => _promptText;
    public string Response => _response;
    public string Mood => _mood;

    // Extra feature: word count is derived from the response rather
    // than stored redundantly, showing abstraction of behavior.
    public int WordCount =>
        string.IsNullOrWhiteSpace(_response)
            ? 0
            : _response.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;

    public Entry(string date, string promptText, string response, string mood = "")
    {
        _date = date;
        _promptText = promptText;
        _response = response;
        _mood = string.IsNullOrWhiteSpace(mood) ? "N/A" : mood;
    }

    /// <summary>
    /// Formats the entry for display on screen.
    /// </summary>
    public override string ToString()
    {
        return $"Date: {_date}\n" +
               $"Prompt: {_promptText}\n" +
               $"Response: {_response}\n" +
               $"Mood: {_mood} (Word count: {WordCount})\n" +
               new string('-', 40);
    }

    /// <summary>
    /// Formats the entry as a single line for the plain-text/custom
    /// separator save format described in the assignment.
    /// </summary>
    public string ToFileLine(string separator)
    {
        return $"{_date}{separator}{_promptText}{separator}{_response}{separator}{_mood}";
    }

    /// <summary>
    /// Parses a single line (using the given separator) back into an Entry.
    /// </summary>
    public static Entry FromFileLine(string line, string separator)
    {
        string[] parts = line.Split(new[] { separator }, StringSplitOptions.None);
        if (parts.Length < 3)
            throw new FormatException("Malformed journal line: " + line);

        string mood = parts.Length >= 4 ? parts[3] : "N/A";
        return new Entry(parts[0], parts[1], parts[2], mood);
    }

    /// <summary>
    /// Formats one CSV row, properly escaping quotes and commas per RFC 4180,
    /// which is one of the "exceed requirements" options in the assignment.
    /// </summary>
    public string ToCsvRow()
    {
        return string.Join(",",
            CsvEscape(_date), CsvEscape(_promptText), CsvEscape(_response), CsvEscape(_mood));
    }

    private static string CsvEscape(string field)
    {
        if (field == null) field = "";
        bool needsQuotes = field.Contains(',') || field.Contains('"') || field.Contains('\n');
        string escaped = field.Replace("\"", "\"\"");
        return needsQuotes ? $"\"{escaped}\"" : escaped;
    }
}


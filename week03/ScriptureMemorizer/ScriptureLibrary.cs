using System;
using System.Collections.Generic;
using System.IO;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Stretch feature: holds a collection of scriptures so the program can
    /// pick one at random each run instead of always memorizing the same
    /// passage. Can also load additional scriptures from a text file.
    /// </summary>
    public class ScriptureLibrary
    {
        private readonly List<Scripture> _scriptures;
        private static readonly Random _random = new Random();

        public ScriptureLibrary()
        {
            _scriptures = new List<Scripture>();
        }

        public int Count => _scriptures.Count;

        public void Add(Scripture scripture)
        {
            _scriptures.Add(scripture);
        }

        /// <summary>
        /// Loads additional scriptures from a pipe-delimited text file.
        /// Expected line format:
        /// Book|Chapter|StartVerse|EndVerse|Text
        /// Example:
        /// John|3|16|16|For God so loved the world...
        /// Malformed lines are silently skipped so a bad file never crashes
        /// the program; the built-in scriptures are always available as a
        /// fallback.
        /// </summary>
        public void LoadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }

            foreach (string line in File.ReadAllLines(path))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split('|');
                if (parts.Length != 5)
                {
                    continue;
                }

                try
                {
                    string book = parts[0].Trim();
                    int chapter = int.Parse(parts[1].Trim());
                    int startVerse = int.Parse(parts[2].Trim());
                    int endVerse = int.Parse(parts[3].Trim());
                    string text = parts[4].Trim();

                    Reference reference = startVerse == endVerse
                        ? new Reference(book, chapter, startVerse)
                        : new Reference(book, chapter, startVerse, endVerse);

                    _scriptures.Add(new Scripture(reference, text));
                }
                catch (FormatException)
                {
                    // Skip malformed lines rather than crashing the program.
                    continue;
                }
            }
        }

        public Scripture GetRandomScripture()
        {
            if (_scriptures.Count == 0)
            {
                throw new InvalidOperationException("The scripture library is empty.");
            }

            int index = _random.Next(_scriptures.Count);
            return _scriptures[index];
        }
    }
}


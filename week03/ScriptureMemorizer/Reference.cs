using System;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Represents the reference portion of a scripture (e.g. "John 3:16" or
    /// "Proverbs 3:5-6"). Supports both a single verse and a verse range.
    /// </summary>
    public class Reference
    {
        public string Book { get; private set; }
        public int Chapter { get; private set; }
        public int StartVerse { get; private set; }
        public int EndVerse { get; private set; }

        /// <summary>
        /// Constructor for a single verse, e.g. Reference("John", 3, 16)
        /// </summary>
        public Reference(string book, int chapter, int verse)
        {
            if (string.IsNullOrWhiteSpace(book))
            {
                throw new ArgumentException("Book name cannot be empty.", nameof(book));
            }

            Book = book;
            Chapter = chapter;
            StartVerse = verse;
            EndVerse = verse;
        }

        /// <summary>
        /// Constructor for a verse range, e.g. Reference("Proverbs", 3, 5, 6)
        /// </summary>
        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            if (string.IsNullOrWhiteSpace(book))
            {
                throw new ArgumentException("Book name cannot be empty.", nameof(book));
            }

            if (endVerse < startVerse)
            {
                throw new ArgumentException("End verse cannot be before start verse.");
            }

            Book = book;
            Chapter = chapter;
            StartVerse = startVerse;
            EndVerse = endVerse;
        }

        /// <summary>
        /// Returns the human-readable form of the reference, e.g. "John 3:16"
        /// or "Proverbs 3:5-6".
        /// </summary>
        public string GetDisplayText()
        {
            if (StartVerse == EndVerse)
            {
                return $"{Book} {Chapter}:{StartVerse}";
            }

            return $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
        }
    }
}


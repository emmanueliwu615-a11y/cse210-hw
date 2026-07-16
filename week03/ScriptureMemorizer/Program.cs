using System;

namespace ScriptureMemorizer
{
    // ============================================================
    // HOW THIS PROGRAM EXCEEDS THE CORE REQUIREMENTS
    // ------------------------------------------------------------
    // 1. Scripture LIBRARY: instead of hard-coding a single scripture,
    // the program builds a small built-in library of several
    // well-known passages and picks one AT RANDOM each time it
    // runs, so practice sessions vary.
    // 2. LOAD FROM FILE: the program also looks for an optional
    // "scriptures.txt" file (pipe-delimited: Book|Chapter|Start|End|Text)
    // next to the executable and, if found, adds every scripture in
    // it to the library. This lets a user grow their own personal
    // library without touching the code.
    // 3. SMARTER HIDING: HideRandomWords only chooses from words that
    // are not already hidden, so every keypress guarantees visible
    // progress instead of occasionally "wasting" a turn re-hiding
    // a word that's already blank.
    // 4. PUNCTUATION-AWARE MASKING: the Word class replaces only
    // letters/digits with underscores, leaving punctuation (periods,
    // commas, colons) intact, so the hidden text still reads with
    // correct sentence structure/rhythm as a memory cue.
    // 5. PROGRESS TRACKING: the display shows a running "% memorized"
    // figure after each round, giving the user positive feedback on
    // how close they are to full memorization.
    // 6. FLEXIBLE PACE: the user can press Enter to hide the default
    // number of words (3), or type a number to hide that many words
    // in one round, letting a confident user speed through practice
    // or a beginner slow it down to one word at a time.
    // ============================================================
    class Program
    {
        private const int DefaultWordsToHidePerRound = 3;

        static void Main(string[] args)
        {
            ScriptureLibrary library = BuildLibrary();

            // Stretch: pull in any user-authored scriptures sitting next
            // to the program, if that file happens to exist.
            library.LoadFromFile("scriptures.txt");

            Scripture scripture = library.GetRandomScripture();

            RunMemorizer(scripture);
        }

        /// <summary>
        /// Runs the main hide-and-reveal loop for a single scripture until
        /// the user quits or the entire scripture is hidden.
        /// </summary>
        private static void RunMemorizer(Scripture scripture)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine();
                Console.WriteLine($"({scripture.GetPercentHidden()}% memorized)");

                if (scripture.IsCompletelyHidden())
                {
                    Console.WriteLine();
                    Console.WriteLine("You've hidden the entire scripture. Great work memorizing it!");
                    break;
                }

                Console.WriteLine();
                Console.Write("Press enter to continue, type a number to hide that many words, or type 'quit' to end: ");
                string input = Console.ReadLine();

                if (string.Equals(input?.Trim(), "quit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                int wordsToHide = DefaultWordsToHidePerRound;
                if (int.TryParse(input?.Trim(), out int typedCount) && typedCount > 0)
                {
                    wordsToHide = typedCount;
                }

                scripture.HideRandomWords(wordsToHide);
            }
        }

        /// <summary>
        /// Builds the built-in library of scriptures available even if no
        /// external file is present.
        /// </summary>
        private static ScriptureLibrary BuildLibrary()
        {
            ScriptureLibrary library = new ScriptureLibrary();

            library.Add(new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world, that he gave his only begotten Son, " +
                "that whosoever believeth in him should not perish, but have everlasting life."));

            library.Add(new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart, and lean not unto thine own understanding. " +
                "In all thy ways acknowledge him, and he shall direct thy paths."));

            library.Add(new Scripture(
                new Reference("Joshua", 1, 9),
                "Have not I commanded thee? Be strong and of a good courage; be not afraid, " +
                "neither be thou dismayed: for the Lord thy God is with thee whithersoever thou goest."));

            library.Add(new Scripture(
                new Reference("Philippians", 4, 13),
                "I can do all things through Christ which strengtheneth me."));

            library.Add(new Scripture(
                new Reference("Psalm", 23, 1, 3),
                "The Lord is my shepherd; I shall not want. He maketh me to lie down in green pastures: " +
                "he leadeth me beside the still waters. He restoreth my soul: he leadeth me in the paths " +
                "of righteousness for his name's sake."));

            return library;
        }
    }
}


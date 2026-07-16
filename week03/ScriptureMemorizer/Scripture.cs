using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Represents a full scripture: a Reference plus the words of its text.
    /// Responsible for hiding words and rendering the current display state.
    /// </summary>
    public class Scripture
    {
        private readonly Reference _reference;
        private readonly List<Word> _words;
        private static readonly Random _random = new Random();

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = text
                .Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
                .Select(w => new Word(w))
                .ToList();
        }

        public bool IsCompletelyHidden()
        {
            return _words.All(w => w.IsHidden);
        }

        /// <summary>
        /// Hides up to numberToHide words that are not already hidden.
        /// (Stretch: only selects from words not yet hidden, so every
        /// button press makes real progress instead of possibly re-hiding
        /// an already-hidden word.)
        /// </summary>
        public void HideRandomWords(int numberToHide)
        {
            List<Word> hidable = _words.Where(w => !w.IsHidden).ToList();
            int count = Math.Min(numberToHide, hidable.Count);

            for (int i = 0; i < count; i++)
            {
                int index = _random.Next(hidable.Count);
                hidable[index].Hide();
                hidable.RemoveAt(index);
            }
        }

        public string GetDisplayText()
        {
            string wordsText = string.Join(" ", _words.Select(w => w.GetDisplayText()));
            return $"{_reference.GetDisplayText()}\n\n{wordsText}";
        }

        /// <summary>
        /// Stretch: reports how much of the scripture is currently hidden,
        /// so the user can track their memorization progress.
        /// </summary>
        public int GetPercentHidden()
        {
            if (_words.Count == 0)
            {
                return 0;
            }

            int hiddenCount = _words.Count(w => w.IsHidden);
            return (int)Math.Round(hiddenCount / (double)_words.Count * 100);
        }
    }
}


namespace ScriptureMemorizer
{
    /// <summary>
    /// Represents a single word within a scripture, tracking whether it is
    /// currently hidden and how to render it either way.
    /// </summary>
    public class Word
    {
        private readonly string _text;
        private bool _isHidden;

        public Word(string text)
        {
            _text = text;
            _isHidden = false;
        }

        public bool IsHidden => _isHidden;

        public void Hide()
        {
            _isHidden = true;
        }

        /// <summary>
        /// Un-hides the word. Not required by the core spec, but included so
        /// a future "reveal" or "reset" feature could reuse this class.
        /// </summary>
        public void Show()
        {
            _isHidden = false;
        }

        /// <summary>
        /// Returns the text to display for this word. When hidden, every
        /// letter or digit is replaced with an underscore, but punctuation
        /// (commas, periods, colons, etc.) is preserved so the sentence
        /// still reads naturally, e.g. "world." becomes "_____.".
        /// </summary>
        public string GetDisplayText()
        {
            if (!_isHidden)
            {
                return _text;
            }

            char[] chars = _text.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (char.IsLetterOrDigit(chars[i]))
                {
                    chars[i] = '_';
                }
            }

            return new string(chars);
        }
    }
}


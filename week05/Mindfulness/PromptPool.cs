using System;
using System.Collections.Generic;

namespace MindfulnessProgram
{
    /// <summary>
    /// Manages a list of prompts/questions and hands them out randomly,
    /// without repeating any entry until every entry in the pool has been
    /// used at least once during the session (exceeds core requirements).
    /// </summary>
    public class PromptPool
    {
        private readonly List<string> _allItems;
        private readonly List<string> _remainingItems;
        private static readonly Random _random = new Random();

        public PromptPool(List<string> items)
        {
            _allItems = new List<string>(items);
            _remainingItems = new List<string>(items);
        }

        public string GetRandom()
        {
            if (_remainingItems.Count == 0)
            {
                _remainingItems.AddRange(_allItems);
            }

            int index = _random.Next(_remainingItems.Count);
            string item = _remainingItems[index];
            _remainingItems.RemoveAt(index);
            return item;
        }
    }
}

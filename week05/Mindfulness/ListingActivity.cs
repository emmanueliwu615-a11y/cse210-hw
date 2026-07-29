using System;
using System.Collections.Generic;

namespace MindfulnessProgram
{
    /// <summary>
    /// Guides the user to think broadly about a topic of strength or
    /// positivity by having them list as many items as they can before
    /// the timer runs out.
    /// </summary>
    public class ListingActivity : Activity
    {
        private static readonly List<string> Prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        private readonly PromptPool _promptPool = new PromptPool(Prompts);

        public ListingActivity()
            : base(
                "Listing Activity",
                "This activity will help you reflect on the good things in your life by " +
                "having you list as many things as you can in a certain area.")
        {
        }

        protected override void PerformActivity()
        {
            Console.WriteLine();
            Console.WriteLine(_promptPool.GetRandom());
            Console.WriteLine();
            Console.WriteLine("You will have a few seconds to think of items...");
            ShowCountDown(5);

            Console.WriteLine();
            Console.WriteLine("Start listing items. Press Enter after each one.");

            List<string> items = new List<string>();
            DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                string? entry = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(entry))
                {
                    items.Add(entry.Trim());
                }
            }

            Console.WriteLine();
            Console.WriteLine($"You listed {items.Count} item(s)!");
        }
    }
}

using System;
using System.Collections.Generic;

namespace MindfulnessProgram
{
    /// <summary>
    /// Guides the user to reflect deeply on a single experience of strength
    /// or resilience by prompting them with a series of follow-up questions.
    /// </summary>
    public class ReflectionActivity : Activity
    {
        private static readonly List<string> Prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private static readonly List<string> Questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        private readonly PromptPool _promptPool = new PromptPool(Prompts);
        private readonly PromptPool _questionPool = new PromptPool(Questions);

        public ReflectionActivity()
            : base(
                "Reflection Activity",
                "This activity will help you reflect on times in your life when you have " +
                "shown strength and resilience. This will help you recognize the power you " +
                "have and how you can use it in other aspects of your life.")
        {
        }

        protected override void PerformActivity()
        {
            Console.WriteLine();
            Console.WriteLine("Consider the following experience:");
            Console.WriteLine();
            Console.WriteLine(_promptPool.GetRandom());
            Console.WriteLine();
            Console.WriteLine("When you have something in mind, press Enter to continue.");
            Console.ReadLine();

            Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
            ShowSpinner(3);

            DateTime endTime = DateTime.Now.AddSeconds(GetDuration());
            while (DateTime.Now < endTime)
            {
                Console.WriteLine();
                Console.Write("> " + _questionPool.GetRandom() + "  ");
                ShowSpinner(5);
            }
        }
    }
}

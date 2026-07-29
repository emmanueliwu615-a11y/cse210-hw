using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessProgram
{
    /// <summary>
    /// Base class for every mindfulness activity. Holds the shared private
    /// state (name, description, duration) and the shared behaviors
    /// (starting message, ending message, spinner, countdown) so that
    /// derived classes only need to implement their unique activity logic.
    /// </summary>
    public abstract class Activity
    {
        private readonly string _name;
        private readonly string _description;
        private int _duration;

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }

        // Template method: every activity runs the same three phases.
        public void Run()
        {
            DisplayStartingMessage();
            PerformActivity();
            DisplayEndingMessage();
        }

        private void DisplayStartingMessage()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------");
            Console.WriteLine($" {_name}");
            Console.WriteLine("--------------------------------");
            Console.WriteLine();
            Console.WriteLine(_description);
            Console.WriteLine();

            _duration = PromptForDuration();

            Console.WriteLine();
            Console.WriteLine("Get ready...");
            ShowSpinner(3);
        }

        private int PromptForDuration()
        {
            int duration = 0;
            bool valid = false;
            while (!valid)
            {
                Console.Write("How long, in seconds, would you like for your session? ");
                string? input = Console.ReadLine();
                valid = int.TryParse(input, out duration) && duration > 0;
                if (!valid)
                {
                    Console.WriteLine("Please enter a positive whole number of seconds.");
                }
            }
            return duration;
        }

        private void DisplayEndingMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Well done!");
            ShowSpinner(3);
            Console.WriteLine();
            Console.WriteLine($"You have completed the {_name} for {_duration} seconds.");
            ShowSpinner(3);

            ActivityLogger.LogActivity(_name, _duration);
        }

        // Each derived activity defines its own unique behavior here.
        protected abstract void PerformActivity();

        protected int GetDuration()
        {
            return _duration;
        }

        protected string GetActivityName()
        {
            return _name;
        }

        // Shared animation #1: a simple rotating spinner.
        protected void ShowSpinner(int seconds)
        {
            List<string> spinnerFrames = new List<string> { "|", "/", "-", "\\" };
            DateTime endTime = DateTime.Now.AddSeconds(seconds);
            int i = 0;
            while (DateTime.Now < endTime)
            {
                Console.Write(spinnerFrames[i % spinnerFrames.Count]);
                Thread.Sleep(250);
                Console.Write("\b \b");
                i++;
            }
        }

        // Shared animation #2: a numeric countdown.
        protected void ShowCountDown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }
    }
}

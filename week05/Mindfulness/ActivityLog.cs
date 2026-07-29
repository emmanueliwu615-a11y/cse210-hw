using System;
using System.Collections.Generic;
using System.IO;

namespace MindfulnessProgram
{
    /// <summary>
    /// Handles saving and loading a persistent log of completed activities
    /// to a text file on disk (exceeds core requirements: statistics + save/load).
    /// </summary>
    public static class ActivityLogger
    {
        private const string LogFile = "activity_log.txt";

        public static void LogActivity(string activityName, int durationSeconds)
        {
            string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}|{activityName}|{durationSeconds}";
            File.AppendAllText(LogFile, line + Environment.NewLine);
        }

        public static void ShowSummary()
        {
            Console.WriteLine();
            Console.WriteLine("=== Activity Log Summary ===");

            if (!File.Exists(LogFile))
            {
                Console.WriteLine("No activity history yet. Complete an activity to start your log!");
                Console.WriteLine("=============================");
                return;
            }

            string[] lines = File.ReadAllLines(LogFile);
            Dictionary<string, int> counts = new Dictionary<string, int>();
            Dictionary<string, int> totalSeconds = new Dictionary<string, int>();

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length < 3)
                {
                    continue;
                }

                string name = parts[1];
                if (!int.TryParse(parts[2], out int duration))
                {
                    continue;
                }

                if (!counts.ContainsKey(name))
                {
                    counts[name] = 0;
                    totalSeconds[name] = 0;
                }

                counts[name]++;
                totalSeconds[name] += duration;
            }

            foreach (KeyValuePair<string, int> entry in counts)
            {
                Console.WriteLine($"{entry.Key}: completed {entry.Value} time(s), " +
                                  $"totaling {totalSeconds[entry.Key]} seconds");
            }

            Console.WriteLine($"Total sessions logged (all time): {lines.Length}");
            Console.WriteLine("=============================");
        }
    }
}

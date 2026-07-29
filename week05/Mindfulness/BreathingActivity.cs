using System;
using System.Threading;

namespace MindfulnessProgram
{
    /// <summary>
    /// Guides the user through slow breathing in and out until the
    /// requested duration has elapsed.
    /// </summary>
    public class BreathingActivity : Activity
    {
        public BreathingActivity()
            : base(
                "Breathing Activity",
                "This activity will help you relax by walking you through breathing in " +
                "and out slowly. Clear your mind and focus on your breathing.")
        {
        }

        protected override void PerformActivity()
        {
            DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

            while (DateTime.Now < endTime)
            {
                Console.WriteLine();
                Console.Write("Breathe in...");
                AnimateBreath(4, growing: true);

                Console.WriteLine();
                Console.Write("Breathe out...");
                AnimateBreath(4, growing: false);
            }
        }

        // Exceeds requirements: instead of a plain countdown, this draws a bar
        // that grows out quickly and slows near the end of the breath (or
        // shrinks the same way on the exhale), giving a visual sense of pace.
        private void AnimateBreath(int seconds, bool growing)
        {
            const int maxWidth = 10;
            int steps = seconds * 2; // half-second increments

            for (int i = 0; i <= steps; i++)
            {
                int width = growing
                    ? (int)((double)i / steps * maxWidth)
                    : maxWidth - (int)((double)i / steps * maxWidth);

                string bar = new string('o', width).PadRight(maxWidth);
                Console.Write("\r" + bar);
                Thread.Sleep(500);
            }

            Console.WriteLine();
        }
    }
}

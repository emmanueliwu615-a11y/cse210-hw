using System;
using System.Collections.Generic;

namespace MindfulnessProgram
{
    /// <summary>
    /// Exceeds requirements: a fourth activity type. Guides the user through
    /// visualizing a peaceful place, prompting them with sensory details to
    /// picture, each followed by a calming pause.
    /// </summary>
    public class VisualizationActivity : Activity
    {
        private static readonly List<string> Prompts = new List<string>
        {
            "Picture yourself somewhere calm, like a beach, a forest, or a quiet room.",
            "Imagine you are standing at the top of a mountain overlooking a valley.",
            "Picture yourself sitting beside a slow-moving river on a warm afternoon.",
            "Imagine you are walking through a garden full of your favorite flowers."
        };

        private static readonly List<string> Details = new List<string>
        {
            "What sounds do you notice around you?",
            "What does the air feel like on your skin?",
            "What colors stand out the most to you?",
            "What scents do you notice in this place?",
            "How does your body feel right now?",
            "What thoughts drift through your mind here?",
            "Is there anyone else with you in this place, or are you alone?",
            "What would you like to remember about this place later?"
        };

        private readonly PromptPool _promptPool = new PromptPool(Prompts);
        private readonly PromptPool _detailPool = new PromptPool(Details);

        public VisualizationActivity()
            : base(
                "Visualization Activity",
                "This activity will help you relax by guiding you to picture a peaceful " +
                "place in as much detail as you can imagine.")
        {
        }

        protected override void PerformActivity()
        {
            Console.WriteLine();
            Console.WriteLine(_promptPool.GetRandom());
            ShowSpinner(3);

            DateTime endTime = DateTime.Now.AddSeconds(GetDuration());
            while (DateTime.Now < endTime)
            {
                Console.WriteLine();
                Console.Write(_detailPool.GetRandom() + "  ");
                ShowSpinner(4);
            }
        }
    }
}

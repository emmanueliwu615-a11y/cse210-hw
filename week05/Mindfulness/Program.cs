using System;

namespace MindfulnessProgram
{
    // ================================================================
    // W05 PROJECT: MINDFULNESS PROGRAM
    //
    // HOW THIS PROGRAM EXCEEDS THE CORE REQUIREMENTS:
    //
    // 1. A FOURTH ACTIVITY was added: VisualizationActivity, which guides
    //    the user through picturing a peaceful place and reflecting on
    //    sensory details, using the same base-class structure as the
    //    other three activities.
    //
    // 2. PERSISTENT ACTIVITY LOG: every completed activity is appended to
    //    activity_log.txt on disk (see ActivityLogger.cs), including
    //    across different runs of the program. A "View Activity Log"
    //    menu option reads that file back and reports how many times each
    //    activity has been completed and the total time spent, so the
    //    log is both saved and loaded (not just tracked in memory).
    //
    // 3. NO REPEATED PROMPTS/QUESTIONS until every entry in a given list
    //    has been shown at least once in the current session. This is
    //    handled generically by the reusable PromptPool class, which both
    //    the Reflection, Listing, and Visualization activities use for
    //    their prompt and question lists.
    //
    // 4. MORE MEANINGFUL BREATHING ANIMATION: instead of a plain
    //    countdown, BreathingActivity draws a bar that grows out during
    //    the inhale and shrinks back down during the exhale, giving the
    //    user a visual sense of pacing their breath.
    //
    // DESIGN NOTES:
    // - Activity.cs is an abstract base class holding all the private,
    //   shared state (name, description, duration) and shared behavior
    //   (starting message, ending message, spinner, countdown) so that
    //   no code is duplicated across the four activity subclasses.
    // - Each subclass only implements PerformActivity(), its own unique
    //   logic, following encapsulation/abstraction principles.
    // ================================================================
    public class Program
    {
        public static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine("     Mindfulness Program");
                Console.WriteLine("================================");
                Console.WriteLine("1) Breathing Activity");
                Console.WriteLine("2) Reflection Activity");
                Console.WriteLine("3) Listing Activity");
                Console.WriteLine("4) Visualization Activity");
                Console.WriteLine("5) View Activity Log");
                Console.WriteLine("6) Quit");
                Console.WriteLine();
                Console.Write("Select a choice from the menu: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        new BreathingActivity().Run();
                        Pause();
                        break;
                    case "2":
                        new ReflectionActivity().Run();
                        Pause();
                        break;
                    case "3":
                        new ListingActivity().Run();
                        Pause();
                        break;
                    case "4":
                        new VisualizationActivity().Run();
                        Pause();
                        break;
                    case "5":
                        ActivityLogger.ShowSummary();
                        Pause();
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("That's not a valid choice. Please try again.");
                        Pause();
                        break;
                }
            }

            Console.WriteLine("Thank you for taking time for mindfulness today. Goodbye!");
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press Enter to return to the menu.");
            Console.ReadLine();
        }
    }
}

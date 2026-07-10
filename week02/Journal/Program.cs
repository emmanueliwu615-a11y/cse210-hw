using System;

// ============================================================
// WAYS THIS PROGRAM EXCEEDS THE CORE REQUIREMENTS:
// 1. Mood tagging: each entry can optionally record how you felt
// (e.g., "Grateful", "Tired"), addressing the "I'm not sure
// what to write / what to focus on" barrier and giving a quick
// emotional snapshot of the journal on Display.
// 2. Multi-format save/load: in addition to the required simple
// "~|~"-separated text file, the journal can save/load as a
// proper .csv (Excel-friendly, with correct comma/quote
// escaping) or as structured .json -- chosen automatically by
// the file extension the user types.
// 3. Word-count + entry-count summary shown after Display, so the
// user gets a small sense of progress/accomplishment.
// 4. Ten prompts instead of the minimum five, for more variety.
// ============================================================

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        bool running = true;
        while (running)
        {
            ShowMenu();
            string choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    WriteNewEntry(journal, promptGenerator);
                    break;
                case "2":
                    journal.Display();
                    break;
                case "3":
                    Console.Write("Enter a filename to save to (e.g., journal.txt, journal.csv, journal.json): ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    break;
                case "4":
                    Console.Write("Enter a filename to load from: ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    break;
                case "5":
                    running = false;
                    Console.WriteLine("Goodbye! Keep writing.");
                    break;
                default:
                    Console.WriteLine("Please choose a valid option (1-5).");
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=== Journal Menu ===");
        Console.WriteLine("1. Write a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save the journal to a file");
        Console.WriteLine("4. Load the journal from a file");
        Console.WriteLine("5. Quit");
        Console.Write("What would you like to do? ");
    }

    static void WriteNewEntry(Journal journal, PromptGenerator promptGenerator)
    {
        string prompt = promptGenerator.GetRandomPrompt();
        Console.WriteLine();
        Console.WriteLine(prompt);
        Console.Write("> ");
        string response = Console.ReadLine();

        Console.Write("Optional: how would you describe your mood? (press Enter to skip): ");
        string mood = Console.ReadLine();

        string date = DateTime.Now.ToShortDateString();
        Entry entry = new Entry(date, prompt, response, mood);
        journal.AddEntry(entry);

        Console.WriteLine("Entry saved!");
    }
}

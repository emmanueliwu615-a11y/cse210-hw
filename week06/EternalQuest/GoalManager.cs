namespace EternalQuest;

// Owns the player's goals and score. Keeps the list of goals and the score
// as private state, and exposes behavior (create goal, record event, list
// goals, save/load) through methods - encapsulation again, this time at the
// "player state" level instead of the individual goal level.
public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    // EXCEEDS REQUIREMENTS: simple leveling system built on top of score.
    // Every 1000 points is a new level, and each level has a silly title
    // (a nod to the "Level 13 Ninja Unicorn" idea from the assignment) so
    // hitting a new tier feels like an event, not just a number going up.
    private static readonly string[] LevelTitles =
    {
        "Wandering Novice",
        "Determined Apprentice",
        "Steady Disciple",
        "Focused Journeyman",
        "Faithful Adept",
        "Resilient Knight",
        "Radiant Champion",
        "Unshaken Sentinel",
        "Ascendant Sage",
        "Ninja Unicorn Supreme",
    };

    // EXCEEDS REQUIREMENTS: badges unlocked at score milestones. Tracked
    // separately so a badge is only announced once, the first time it's
    // crossed, instead of every time the score is displayed.
    private static readonly (int Threshold, string Name)[] Badges =
    {
        (100, "First Steps"),
        (500, "Building Momentum"),
        (1000, "Century Club"),
        (2500, "Unstoppable"),
        (5000, "Legend of the Quest"),
    };

    private HashSet<string> _earnedBadges;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _earnedBadges = new HashSet<string>();
    }

    public void Run()
    {
        DisplayWelcome();

        bool keepRunning = true;
        while (keepRunning)
        {
            DisplayMenu();
            string choice = Console.ReadLine() ?? string.Empty;
            Console.WriteLine();

            switch (choice.Trim())
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoals();
                    break;
                case "3":
                    RecordEvent();
                    break;
                case "4":
                    DisplayScore();
                    break;
                case "5":
                    SaveGoals();
                    break;
                case "6":
                    LoadGoals();
                    break;
                case "7":
                    keepRunning = false;
                    Console.WriteLine("Keep questing. See you next time!");
                    break;
                default:
                    Console.WriteLine("That's not a valid choice. Try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private void DisplayWelcome()
    {
        Console.WriteLine("==========================================");
        Console.WriteLine("           THE ETERNAL QUEST");
        Console.WriteLine("==========================================");
        Console.WriteLine("Track your goals. Earn your points. Level up.");
        Console.WriteLine();
    }

    private void DisplayMenu()
    {
        Console.WriteLine("Menu Options:");
        Console.WriteLine("  1. Create a New Goal");
        Console.WriteLine("  2. List Goals");
        Console.WriteLine("  3. Record an Event");
        Console.WriteLine("  4. Show Score / Level");
        Console.WriteLine("  5. Save Goals");
        Console.WriteLine("  6. Load Goals");
        Console.WriteLine("  7. Quit");
        Console.Write("Select an option: ");
    }

    private void CreateGoal()
    {
        Console.WriteLine("The type of goal you would like to create:");
        Console.WriteLine("  1. Simple Goal        (done once, e.g. 'Run a marathon')");
        Console.WriteLine("  2. Eternal Goal        (never finishes, e.g. 'Read scriptures')");
        Console.WriteLine("  3. Checklist Goal      (done N times, e.g. 'Attend the temple x10')");
        Console.WriteLine("  4. Negative Goal       (bad habit, costs points, e.g. 'Skipped workout')");
        Console.WriteLine("  5. Progress Goal       (build up to one big target, e.g. 'Train for a marathon')");
        Console.Write("Select a type: ");
        string typeChoice = (Console.ReadLine() ?? string.Empty).Trim();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine() ?? "Untitled Goal";

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine() ?? string.Empty;

        Console.Write("How many points is this goal worth? ");
        int points = ReadInt();

        Goal? newGoal = null;

        switch (typeChoice)
        {
            case "1":
                newGoal = new SimpleGoal(name, description, points);
                break;
            case "2":
                newGoal = new EternalGoal(name, description, points);
                break;
            case "3":
                Console.Write("How many times must it be completed? ");
                int target = ReadInt();
                Console.Write("What is the bonus for completing it? ");
                int bonus = ReadInt();
                newGoal = new ChecklistGoal(name, description, points, target, bonus);
                break;
            case "4":
                newGoal = new NegativeGoal(name, description, points);
                break;
            case "5":
                Console.Write("What is the total target amount (e.g. 26 for 26 miles)? ");
                int progressTarget = ReadInt();
                Console.Write("What is the unit (e.g. miles, pages, chapters)? ");
                string unit = Console.ReadLine() ?? "units";
                Console.Write("What is the completion bonus? ");
                int progressBonus = ReadInt();
                newGoal = new ProgressGoal(name, description, points, progressTarget, progressBonus, unit);
                break;
            default:
                Console.WriteLine("Not a recognized goal type. Goal not created.");
                return;
        }

        _goals.Add(newGoal);
        Console.WriteLine($"Goal \"{name}\" created!");
    }

    private void ListGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You don't have any goals yet. Create one first!");
            return;
        }

        Console.WriteLine("Your Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    private void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You don't have any goals yet. Create one first!");
            return;
        }

        ListGoals();
        Console.Write("Which goal did you accomplish? (enter the number) ");
        int index = ReadInt() - 1;

        if (index < 0 || index >= _goals.Count)
        {
            Console.WriteLine("That's not a valid goal number.");
            return;
        }

        Goal goal = _goals[index];
        int pointsEarned;

        // ProgressGoal records a variable amount instead of a flat "one
        // event," so it needs its own prompt. Everything else goes through
        // the shared RecordEvent() the base class defines - polymorphism at
        // work: this code doesn't know or care which concrete type it has.
        if (goal is ProgressGoal progressGoal)
        {
            Console.Write($"How much progress did you make (in its units)? ");
            int amount = ReadInt();
            pointsEarned = progressGoal.RecordEvent(amount);
        }
        else
        {
            pointsEarned = goal.RecordEvent();
        }

        int previousScore = _score;
        _score += pointsEarned;
        if (_score < 0)
        {
            _score = 0;
        }

        if (pointsEarned >= 0)
        {
            Console.WriteLine($"Congratulations! You earned {pointsEarned} points!");
        }
        else
        {
            Console.WriteLine($"That one cost you {-pointsEarned} points. Shake it off and keep going.");
        }

        CheckForLevelUp(previousScore, _score);
        CheckForNewBadges();

        if (goal.IsComplete())
        {
            Console.WriteLine($"Goal \"{goal.ShortName}\" is now complete! Well done.");
        }
    }

    private void DisplayScore()
    {
        Console.WriteLine($"Score: {_score} points");
        Console.WriteLine($"Level {GetLevel(_score)}: {GetLevelTitle(_score)}");

        if (_earnedBadges.Count > 0)
        {
            Console.WriteLine($"Badges earned: {string.Join(", ", _earnedBadges)}");
        }
    }

    private void CheckForLevelUp(int previousScore, int newScore)
    {
        int previousLevel = GetLevel(previousScore);
        int newLevel = GetLevel(newScore);

        if (newLevel > previousLevel)
        {
            Console.WriteLine();
            Console.WriteLine("*** LEVEL UP! ***");
            Console.WriteLine($"You are now Level {newLevel}: {GetLevelTitle(newScore)}!");
        }
    }

    private void CheckForNewBadges()
    {
        foreach (var (threshold, name) in Badges)
        {
            if (_score >= threshold && !_earnedBadges.Contains(name))
            {
                _earnedBadges.Add(name);
                Console.WriteLine($"New badge unlocked: \"{name}\"!");
            }
        }
    }

    private int GetLevel(int score)
    {
        return (score / 1000) + 1;
    }

    private string GetLevelTitle(int score)
    {
        int level = GetLevel(score);
        int index = Math.Min(level - 1, LevelTitles.Length - 1);
        return LevelTitles[index];
    }

    private void SaveGoals()
    {
        Console.Write("What file name should we save your goals to? ");
        string filename = Console.ReadLine() ?? "goals.txt";

        using StreamWriter writer = new StreamWriter(filename);
        writer.WriteLine(_score);
        writer.WriteLine(string.Join(",", _earnedBadges));

        foreach (Goal goal in _goals)
        {
            writer.WriteLine(goal.GetStringRepresentation());
        }

        Console.WriteLine($"Goals saved to {filename}.");
    }

    private void LoadGoals()
    {
        Console.Write("What file name should we load your goals from? ");
        string filename = Console.ReadLine() ?? "goals.txt";

        if (!File.Exists(filename))
        {
            Console.WriteLine($"Could not find a file named {filename}.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        if (lines.Length == 0)
        {
            Console.WriteLine("That file is empty.");
            return;
        }

        _goals.Clear();
        _earnedBadges.Clear();

        _score = int.Parse(lines[0]);

        if (lines.Length > 1 && lines[1].Trim().Length > 0)
        {
            foreach (string badge in lines[1].Split(','))
            {
                _earnedBadges.Add(badge);
            }
        }

        for (int i = 2; i < lines.Length; i++)
        {
            Goal? goal = ParseGoal(lines[i]);
            if (goal != null)
            {
                _goals.Add(goal);
            }
        }

        Console.WriteLine($"Loaded {_goals.Count} goal(s) from {filename}.");
    }

    // Turns a saved line back into the correct concrete Goal subtype. Kept
    // here (rather than on Goal itself) because building the right derived
    // class from a type tag is a manager-level responsibility, not
    // something an individual goal needs to know how to do.
    private Goal? ParseGoal(string line)
    {
        string[] parts = line.Split(':');
        if (parts.Length == 0)
        {
            return null;
        }

        string type = parts[0];

        try
        {
            switch (type)
            {
                case "SimpleGoal":
                    return new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4]));
                case "EternalGoal":
                    return new EternalGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]));
                case "ChecklistGoal":
                    return new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6]));
                case "NegativeGoal":
                    return new NegativeGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]));
                case "ProgressGoal":
                    return new ProgressGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]), parts[6], int.Parse(parts[7]));
                default:
                    return null;
            }
        }
        catch (Exception)
        {
            // A corrupted or hand-edited line shouldn't crash the whole load.
            return null;
        }
    }

    private int ReadInt()
    {
        string input = Console.ReadLine() ?? string.Empty;
        return int.TryParse(input, out int result) ? result : 0;
    }
}

namespace EternalQuest;

// A goal that is never "finished," e.g. "Read scriptures" for 100 points
// every time it's recorded. Tracks how many times it's been done purely
// for display/streak purposes - it never sets IsComplete to true.
public class EternalGoal : Goal
{
    private int _timesCompleted;

    public int TimesCompleted => _timesCompleted;

    public EternalGoal(string shortName, string description, int points)
        : base(shortName, description, points)
    {
        _timesCompleted = 0;
    }

    public EternalGoal(string shortName, string description, int points, int timesCompleted)
        : base(shortName, description, points)
    {
        _timesCompleted = timesCompleted;
    }

    public override bool IsComplete()
    {
        // Eternal goals are, by design, never complete.
        return false;
    }

    public override int RecordEvent()
    {
        _timesCompleted++;
        return Points;
    }

    public override string GetDetailsString()
    {
        return $"{CheckboxLabel()} {ShortName} ({Description}) -- recorded {_timesCompleted} times";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{ShortName}:{Description}:{Points}:{_timesCompleted}";
    }
}

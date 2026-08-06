namespace EternalQuest;

// EXCEEDS REQUIREMENTS: A "negative" goal for bad habits you're trying to
// stop, e.g. "Skipped scripture study" costs 50 points every time it's
// recorded. Like EternalGoal it never completes, but RecordEvent()
// subtracts points instead of adding them, and the manager is written so a
// player's score is never allowed to drop below zero.
public class NegativeGoal : Goal
{
    private int _timesRecorded;

    public NegativeGoal(string shortName, string description, int points)
        : base(shortName, description, points)
    {
        _timesRecorded = 0;
    }

    public NegativeGoal(string shortName, string description, int points, int timesRecorded)
        : base(shortName, description, points)
    {
        _timesRecorded = timesRecorded;
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override int RecordEvent()
    {
        _timesRecorded++;
        // Negative points - the caller (GoalManager) applies this to score.
        return -Points;
    }

    public override string GetDetailsString()
    {
        return $"{CheckboxLabel()} {ShortName} ({Description}) -- slipped up {_timesRecorded} times";
    }

    public override string GetStringRepresentation()
    {
        return $"NegativeGoal:{ShortName}:{Description}:{Points}:{_timesRecorded}";
    }
}

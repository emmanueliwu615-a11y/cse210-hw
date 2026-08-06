namespace EternalQuest;

// A goal that is done once and then finished, e.g. "Run a marathon" for 1000 points.
public class SimpleGoal : Goal
{
    public SimpleGoal(string shortName, string description, int points)
        : base(shortName, description, points)
    {
    }

    // Loading a saved goal that was already marked complete needs to be able
    // to restore that state without re-awarding points.
    public SimpleGoal(string shortName, string description, int points, bool isComplete)
        : base(shortName, description, points)
    {
        if (isComplete)
        {
            MarkComplete();
        }
    }

    public override int RecordEvent()
    {
        if (IsComplete())
        {
            // Already finished - recording it again earns nothing.
            return 0;
        }

        MarkComplete();
        return Points;
    }

    public override string GetDetailsString()
    {
        return $"{CheckboxLabel()} {ShortName} ({Description})";
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{ShortName}:{Description}:{Points}:{IsComplete()}";
    }
}

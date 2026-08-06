namespace EternalQuest;

// A goal that must be recorded a set number of times before it counts as
// complete, e.g. "Attend the temple" 10 times for 50 points each time, plus
// a 500 point bonus on the 10th.
public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string shortName, string description, int points, int target, int bonus)
        : base(shortName, description, points)
    {
        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
    }

    public ChecklistGoal(string shortName, string description, int points, int target, int bonus, int amountCompleted)
        : base(shortName, description, points)
    {
        _amountCompleted = amountCompleted;
        _target = target;
        _bonus = bonus;

        if (_amountCompleted >= _target)
        {
            MarkComplete();
        }
    }

    public override int RecordEvent()
    {
        if (IsComplete())
        {
            return 0;
        }

        _amountCompleted++;
        int pointsEarned = Points;

        if (_amountCompleted >= _target)
        {
            MarkComplete();
            pointsEarned += _bonus;
        }

        return pointsEarned;
    }

    public override string GetDetailsString()
    {
        return $"{CheckboxLabel()} {ShortName} ({Description}) -- Completed {_amountCompleted}/{_target} times";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{ShortName}:{Description}:{Points}:{_target}:{_bonus}:{_amountCompleted}";
    }
}

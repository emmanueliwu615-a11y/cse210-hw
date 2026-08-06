namespace EternalQuest;

// EXCEEDS REQUIREMENTS: A goal that tracks incremental progress toward one
// big target, e.g. "Run a marathon" measured in miles trained (26 total).
// Unlike ChecklistGoal (a fixed number of discrete repetitions), each event
// can report a variable amount of progress, and points scale with how much
// progress was actually made that time. A bonus is awarded once the full
// target is reached.
public class ProgressGoal : Goal
{
    private int _currentProgress;
    private int _targetProgress;
    private int _bonus;
    private string _unit;

    public ProgressGoal(string shortName, string description, int pointsPerUnit, int targetProgress, int bonus, string unit)
        : base(shortName, description, pointsPerUnit)
    {
        _currentProgress = 0;
        _targetProgress = targetProgress;
        _bonus = bonus;
        _unit = unit;
    }

    public ProgressGoal(string shortName, string description, int pointsPerUnit, int targetProgress, int bonus, string unit, int currentProgress)
        : base(shortName, description, pointsPerUnit)
    {
        _currentProgress = currentProgress;
        _targetProgress = targetProgress;
        _bonus = bonus;
        _unit = unit;

        if (_currentProgress >= _targetProgress)
        {
            MarkComplete();
        }
    }

    // Records a chunk of progress (e.g. "ran 4 miles today") and returns the
    // points earned for that chunk, plus the bonus if this pushes the goal
    // over its target.
    public int RecordEvent(int amount)
    {
        if (IsComplete())
        {
            return 0;
        }

        int amountToApply = Math.Min(amount, _targetProgress - _currentProgress);
        _currentProgress += amountToApply;
        int pointsEarned = amountToApply * Points;

        if (_currentProgress >= _targetProgress)
        {
            MarkComplete();
            pointsEarned += _bonus;
        }

        return pointsEarned;
    }

    // Satisfies the abstract base contract; the menu calls the int-overload
    // above so it can prompt for an amount, but this keeps ProgressGoal
    // usable anywhere a plain Goal is expected.
    public override int RecordEvent()
    {
        return RecordEvent(1);
    }

    public override string GetDetailsString()
    {
        return $"{CheckboxLabel()} {ShortName} ({Description}) -- {_currentProgress}/{_targetProgress} {_unit}";
    }

    public override string GetStringRepresentation()
    {
        return $"ProgressGoal:{ShortName}:{Description}:{Points}:{_targetProgress}:{_bonus}:{_unit}:{_currentProgress}";
    }
}

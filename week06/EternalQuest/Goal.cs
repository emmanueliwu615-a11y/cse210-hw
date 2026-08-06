namespace EternalQuest;

// Base class for every kind of goal. Holds the state and behavior that all
// goals share (name, description, point value, completion state) and defines
// the "shape" that every derived goal type must follow. Fields are private
// so derived classes and outside code must go through properties/methods
// (encapsulation) instead of reaching into the internals directly.
public abstract class Goal
{
    private string _shortName;
    private string _description;
    private int _points;
    private bool _isComplete;

    public string ShortName => _shortName;
    public string Description => _description;
    public int Points => _points;

    protected Goal(string shortName, string description, int points)
    {
        _shortName = shortName;
        _description = description;
        _points = points;
        _isComplete = false;
    }

    // Marks the goal complete. Simple/Checklist goals use this; Eternal and
    // Negative goals never become "complete" so they simply don't call it.
    protected void MarkComplete()
    {
        _isComplete = true;
    }

    public virtual bool IsComplete()
    {
        return _isComplete;
    }

    // Every goal type records an event differently (simple goals finish,
    // eternal goals repeat forever, checklist goals count toward a target,
    // etc.) so each derived class must supply its own version of this
    // method. This is the polymorphism the assignment requires: the
    // GoalManager just calls RecordEvent() on whatever Goal it has, without
    // needing to know which concrete type it is.
    public abstract int RecordEvent();

    // Each derived class also controls exactly how it prints its own
    // status line (checkbox vs. progress count, etc.).
    public abstract string GetDetailsString();

    // Each derived class controls how it serializes itself for saving,
    // since different goal types carry different extra data.
    public abstract string GetStringRepresentation();

    // Shared helper so every derived class's GetDetailsString() can build
    // the "[X] Name (Description)" / "[ ] Name (Description)" line the
    // same way, instead of repeating this formatting in every subclass.
    protected string CheckboxLabel()
    {
        return IsComplete() ? "[X]" : "[ ]";
    }
}

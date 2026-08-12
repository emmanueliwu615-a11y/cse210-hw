using System;

// Base class for all exercise activities.
// Holds the attributes shared by every activity (date and length),
// and declares the calculation methods that each derived activity
// must implement in its own way.
public abstract class Activity
{
    // Private fields -> encapsulation. Nothing outside this class
    // can reach in and change these directly.
    private DateTime _date;
    private int _lengthMinutes;

    public Activity(DateTime date, int lengthMinutes)
    {
        _date = date;
        _lengthMinutes = lengthMinutes;
    }

    // Read-only access for derived classes / callers that need the raw values.
    public DateTime Date => _date;
    public int LengthMinutes => _lengthMinutes;

    // These are not implemented here because the math is different for
    // every activity. Each derived class overrides them (polymorphism).
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Every activity can build its summary the same way, just by calling
    // the (overridden) calculation methods above. Because it's virtual,
    // a derived class could still override it if it ever needed a
    // different summary format, but none of them need to here.
    public virtual string GetSummary()
    {
        return $"{_date:dd MMM yyyy} {GetType().Name} ({_lengthMinutes} min) - " +
               $"Distance: {GetDistance():F1} miles, " +
               $"Speed: {GetSpeed():F1} mph, " +
               $"Pace: {GetPace():F2} min per mile";
    }
}

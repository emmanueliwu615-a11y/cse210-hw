using System;

// Cycling stores speed directly (in mph). Distance and pace are
// calculated from that speed and the length of the activity.
public class Cycling : Activity
{
    private double _speedMph;

    public Cycling(DateTime date, int lengthMinutes, double speedMph)
        : base(date, lengthMinutes)
    {
        _speedMph = speedMph;
    }

    public override double GetSpeed()
    {
        return _speedMph;
    }

    public override double GetDistance()
    {
        // Distance = speed * (minutes / 60)
        return _speedMph * (LengthMinutes / 60.0);
    }

    public override double GetPace()
    {
        // Pace (min per mile) = 60 / speed
        return 60 / _speedMph;
    }
}

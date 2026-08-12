using System;

// Swimming stores the number of laps. Distance, speed, and pace are
// all calculated from the laps (each lap is 50 meters) and the length
// of the activity.
public class Swimming : Activity
{
    private int _laps;
    private const double LapLengthMeters = 50;
    private const double MetersPerMile = 1609.34;

    public Swimming(DateTime date, int lengthMinutes, int laps)
        : base(date, lengthMinutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        // total meters swum, converted to miles
        double totalMeters = _laps * LapLengthMeters;
        return totalMeters / MetersPerMile;
    }

    public override double GetSpeed()
    {
        // Speed (mph) = (distance / minutes) * 60
        return (GetDistance() / LengthMinutes) * 60;
    }

    public override double GetPace()
    {
        // Pace (min per mile) = minutes / distance
        return LengthMinutes / GetDistance();
    }
}

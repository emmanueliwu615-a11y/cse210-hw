using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // One list holding every activity type. This only works because
        // Running, Cycling, and Swimming all inherit from Activity -
        // that's polymorphism in action: the list doesn't need to know
        // (or care) which specific activity each item really is.
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            new Cycling(new DateTime(2022, 11, 3), 45, 12.5),
            new Swimming(new DateTime(2022, 11, 3), 30, 20),
            new Running(new DateTime(2022, 11, 5), 25, 2.4),
            new Cycling(new DateTime(2022, 11, 6), 60, 15.0)
        };

        foreach (Activity activity in activities)
        {
            // GetSummary() is defined once on the base class but calls
            // GetDistance/GetSpeed/GetPace, which each derived class
            // overrides with its own math - so the same call produces
            // a different, correct result for every activity type.
            Console.WriteLine(activity.GetSummary());
        }
    }
}

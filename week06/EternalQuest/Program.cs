// EXCEEDS REQUIREMENTS - summary of what was added beyond the base spec:
//
// 1. Two extra goal types beyond the required Simple/Eternal/Checklist:
//      - NegativeGoal: for bad habits. Recording it SUBTRACTS points
//        instead of adding them (e.g. "Skipped scripture study" -50 pts).
//        Score is never allowed to go below zero.
//      - ProgressGoal: for goals you work toward incrementally with a
//        variable amount each time (e.g. training miles toward a 26 mile
//        marathon), rather than a fixed number of discrete repetitions.
//        Points scale with how much progress was actually logged, and a
//        bonus is awarded once the target is fully reached.
//
// 2. A leveling system layered on top of the score: every 1000 points is
//    a new level, and each level has its own title (Wandering Novice ...
//    all the way up to "Ninja Unicorn Supreme," a nod to the example in
//    the assignment). Leveling up prints a little celebration banner.
//
// 3. A badge/achievement system that unlocks one-time badges at score
//    milestones (First Steps, Century Club, Legend of the Quest, etc.),
//    tracked so each badge is only announced the first time it's earned.
//    Badges are saved and loaded along with the goals and score so
//    progress persists between sessions.
//
// 4. Save/Load persists everything needed to fully restore a session:
//    score, earned badges, and every goal's type-specific progress
//    (e.g. a Checklist goal remembers exactly how many times it's been
//    completed, a Progress goal remembers exactly how far along it is).
//
// All of this is built on the required OOP structure: Goal is an abstract
// base class holding the shared private state (name, description, points,
// completion flag) behind encapsulated properties/methods; every goal type
// is a derived class; and RecordEvent()/GetDetailsString()/
// GetStringRepresentation() are abstract methods that each subclass
// overrides with its own behavior (polymorphism), so GoalManager never
// needs to know or care which concrete goal type it's holding.

using EternalQuest;

GoalManager manager = new GoalManager();
manager.Run();

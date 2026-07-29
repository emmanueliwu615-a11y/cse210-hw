using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("How to Cook Jollof Rice", "Chef Amaka", 612);
        video1.AddComment(new Comment("FoodieTee", "This is exactly how my mom makes it!"));
        video1.AddComment(new Comment("LagosEats", "Finally a recipe that doesn't skip steps."));
        video1.AddComment(new Comment("Kunle_23", "Tried this, came out amazing. Thank you!"));
        video1.AddComment(new Comment("Ada B", "Can you do a video on egusi soup next?"));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("Beginner Guitar Lessons - Episode 1", "Marcus Reed", 845);
        video2.AddComment(new Comment("StringsFan", "Great pacing for beginners."));
        video2.AddComment(new Comment("Newbie_Guitarist", "I finally understand chord transitions."));
        video2.AddComment(new Comment("MusicLover99", "Your explanations are so clear."));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("Top 10 Programming Tips", "Sarah Chen", 723);
        video3.AddComment(new Comment("DevGuy", "Tip #7 changed how I write functions."));
        video3.AddComment(new Comment("CodeNewbie", "Wish I saw this before starting my course."));
        video3.AddComment(new Comment("TechExplorer", "Subscribed after watching this!"));
        video3.AddComment(new Comment("PythonPro", "Solid list, would add unit testing too."));
        videos.Add(video3);

        // Video 4
        Video video4 = new Video("Exploring Lagos Real Estate Trends", "Property Insights NG", 934);
        video4.AddComment(new Comment("InvestorJide", "This matches what I'm seeing in Lekki."));
        video4.AddComment(new Comment("DiasporaBuyer", "Very helpful for those of us abroad."));
        video4.AddComment(new Comment("RealEstateGuru", "Would love a follow-up on Abuja."));
        videos.Add(video4);

        // Display each video's info and comments
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLength()} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($" - {comment.GetName()}: {comment.GetText()}");
            }

            Console.WriteLine(new string('-', 50));
        }
    }
}

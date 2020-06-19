using System;
using System.Collections.Generic;

namespace ahk.common
{
    public class AhkResult
    {
        private readonly List<string> problems = new List<string>();

        public AhkResult(string exerciseName)
            => this.ExerciseName = exerciseName;

        public string ExerciseName { get; }
        public int Points { get; private set; } = 0;
        public IReadOnlyCollection<string> Problems => problems;

        public void AddProblem(string description)
        {
            description = truncateMessage(description);
            Console.WriteLine(description);
            problems.Add(description);
        }

        public void AddProblem(Exception ex, string description)
        {
            description = truncateMessage(description);
            Log(description);
            Log(ex);
            problems.Add(description);
        }

        public void Log(Exception ex)
        {
            if (ex != null)
            {
                Log(ex.Message);
                if (ex.InnerException != null)
                    Log(ex.InnerException);
            }
        }

        public void Log(string description)
            => Console.WriteLine(truncateMessage(description));

        public void AddPoints(int pointToAdd)
            => Points += pointToAdd;

        public void ResetPointToZero()
            => Points = 0;

        private static string truncateMessage(string description)
        {
            if (description == null)
                return string.Empty;
            if (description.Length > 1000)
                return description.Substring(0, 980) + " [... truncated]";
            return description;
        }
    }
}

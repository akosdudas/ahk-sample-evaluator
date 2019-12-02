namespace evaluator
{
    class Program
    {
        static void Main(string[] args)
        {
            // The solution is expected at a speicifc path. This is ensured by the initial code structure handed out using for example GitHub Classroom.
            var solutionFilePath = "solution.txt";

            int points = 0;
            string errormessage = "";
            if (System.IO.File.Exists(solutionFilePath))
            {
                var solution = System.IO.File.ReadAllText(solutionFilePath);

                // 1 point if the file exists
                points += 1;

                if (solution.Contains("42"))
                {
                    // more points for the correct solution
                    points += 1;
                }
            }
            else
            {
                // custom error message for explaining the problems
                errormessage = "Expected solution file does not exist.";
            }

            // The output of the evaluation is a text file at a well-known path with the following syntax.
            // The file can contain multiple lines, all with the same syntax for multiple exercises.
            System.IO.File.WriteAllText("result.txt", $"###ahk#Exercise 1#{points}#{errormessage}");
        }
    }
}

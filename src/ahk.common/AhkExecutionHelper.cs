using System;
using System.Threading.Tasks;

namespace ahk.common
{
    public static class AhkExecutionHelper
    {
        public async static Task<int> Execute(params AhkEvaluationTask[] tasksToExecute)
        {
            Console.WriteLine("Starting Ahk evaluation...");

            if (tasksToExecute == null || tasksToExecute.Length == 0)
            {
                AhkOutputWriter.WriteInconclusiveResult("N/A", "Hibas teszt alkalmazas. Error in the evaluation application.");
                return -1;
            }

            try
            {
                foreach (var test in tasksToExecute)
                    await executeSafe(test);

                Console.WriteLine("Evaluation completed.");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in evaluation.");
                Console.WriteLine(ex);
                return -1;
            }
        }

        private static async Task executeSafe(AhkEvaluationTask evalTask)
        {
            var result = new AhkResult(evalTask.ExerciseName);
            try
            {
                await evalTask.Execute(result);

                if (!evalTask.IsPreProcess)
                    result.WriteToFile();
            }
            catch (Exception ex)
            {
                AhkOutputWriter.WriteInconclusiveResult(evalTask.ExerciseName, "Unexpected error in evaluation. Nem vart hiba a kiertekeles kozben.");

                Console.WriteLine("Error in evaluation. Kiertekelesben hiba tortent.");
                Console.WriteLine(ex);

                if (evalTask.IsPreProcess)
                    throw;
            }
        }
    }
}

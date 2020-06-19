using System;
using System.Threading.Tasks;
using adatvez.Helpers;
using ahk.common;

namespace evaluator
{
    internal class EvaluatorEx1
    {
        /// <summary>
        /// The name of the exercise; used in the summary results.
        /// </summary>
        public const string AhkExerciseName = @"Feladat 1 - Exercise 1";

        public static async Task Execute(AhkResult ahkResult)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("###### Feladat 1 ###### Exercise 1 ######");

            try
            {
                await testGetWithNotFound(ahkResult);
                await testGetWithSuccess(ahkResult);

                // screenshot is mandatory
                if (!ScreenshotValidator.IsScreenshotPresent("screenshot.png", "screenshot.png", ahkResult))
                    ahkResult.ResetPointToZero();
            }
            catch (MissingMethodException ex) // expected problem, violates contract, solution evaluation ignored
            {
                ahkResult.AddProblem(ex, "Nem megengedett kodot valtoztattal. Changed code that you should not have.");
            }
            catch (TypeLoadException ex) // expected problem, violates contract, solution evaluation ignored
            {
                ahkResult.AddProblem(ex, "Nem megengedett kodot valtoztattal. Changed code that you should not have.");
            }
        }

        /// <summary>
        /// GET /api/sample 404
        /// </summary>
        private static async Task testGetWithNotFound(AhkResult ahkResult)
        {
            using (var scope = WebAppInit.GetRequestScope())
            {
                var restGetNoResult = await scope.HttpClient.TryGet<homework.Model.SampleData>("/api/sample?input=no", ahkResult, allowNotFound: true);
                if (restGetNoResult.Success)
                {
                    ahkResult.AddPoints(1);
                    ahkResult.Log("GET /api/sample with notfound");
                }
            }
        }

        /// <summary>
        /// GET /api/sample 200
        /// </summary>
        private static async Task testGetWithSuccess(AhkResult ahkResult)
        {
            using (var scope = WebAppInit.GetRequestScope())
            {
                var restGetResult = await scope.HttpClient.TryGet<homework.Model.SampleData>("/api/sample?input=apple", ahkResult);
                if (restGetResult.Success)
                {
                    if (restGetResult.Value != null && restGetResult.Value.Name.Equals("apple", StringComparison.OrdinalIgnoreCase))
                    {
                        ahkResult.AddPoints(1);
                        ahkResult.Log("GET /api/sample with success");
                    }
                    else
                    {
                        ahkResult.Log("Returned item does not match expected data.");
                    }
                }
            }
        }
    }
}

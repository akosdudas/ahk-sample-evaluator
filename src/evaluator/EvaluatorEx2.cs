using System;
using System.Threading.Tasks;
using adatvez.Helpers;
using ahk.common;

namespace evaluator
{
    internal class EvaluatorEx2
    {
        public const string AhkExerciseName = @"Feladat 2 - Exercise 2";

        public static async Task Execute(AhkResult ahkResult)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("###### Feladat 2 ###### Exercise 2 ######");

            try
            {
                await testDelete(ahkResult);
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
        /// DELETE /api/sample
        /// </summary>
        private static async Task testDelete(AhkResult ahkResult)
        {
            using (var scope = WebAppInit.GetRequestScope())
            {
                var deleteOkResponse = await scope.HttpClient.TryDelete($"/api/sample?input=apple", ahkResult);
                if (deleteOkResponse.Success)
                {
                    ahkResult.AddPoints(2);
                    ahkResult.Log("DELETE /api/sample ok");
                }
                else
                {
                    ahkResult.AddProblem("DELETE /api/sample valasz tartalma hibas. DELETE /api/sample yields invalid reponse.");
                }
            }
        }
    }
}

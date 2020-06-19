using System.Threading.Tasks;
using ahk.common;

namespace evaluator
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            return await AhkExecutionHelper.Execute(
                        new AhkEvaluationTask(WebAppInit.AhkExerciseName, WebAppInit.StartWebApp, isPreProcess: true),
                        new AhkEvaluationTask(EvaluatorEx1.AhkExerciseName, EvaluatorEx1.Execute),
                        new AhkEvaluationTask(EvaluatorEx2.AhkExerciseName, EvaluatorEx2.Execute));
        }
    }
}

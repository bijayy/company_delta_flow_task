using System.Threading;
using System.Threading.Tasks;

using company_delta_flow_task_blazor.ViewModels;

namespace company_delta_flow_task_blazor.Data.Providers
{
	public interface IQuizProvider
	{
		Task<bool> ConfigureQuizForUserAsync(ConfigureUserQuizViewModel configureUserQuizViewModel, CancellationToken cancellationToken = default);
		Task<bool> CreateQuizAsync(QuizViewModel quizViewModel, CancellationToken cancellationToken = default);
		Task<bool> CreateQuestionAsync(QuestionViewModel questionViewModel, CancellationToken cancellationToken = default);
		Task<bool> CreateAnswerAsync(AnswerViewModel answerViewModel, CancellationToken cancellationToken = default);
		Task<bool> CalculateQuizResultAsync(UserAnswerViewModel userAnswerViewModel, CancellationToken cancellationToken = default);
	
		Task<QuizViewModel> GetQuizByUserIdAsync(long userId, CancellationToken cancellationToken = default);
	}
}

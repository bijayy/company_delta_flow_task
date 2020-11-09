using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using company_delta_flow_task_blazor.Data.Providers;
using company_delta_flow_task_blazor.Services;
using company_delta_flow_task_blazor.ViewModels;

using Microsoft.AspNetCore.Components;

namespace company_delta_flow_task_blazor.Pages.Quiz
{
	public class TakeQuizComponentBase : ComponentBase
	{
		CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

		[Inject]
		protected UserStateService userStateService { get; set; }

		[Inject]
		protected IQuizProvider quizProvider { get; set; }

		[Inject]
		protected NavigationManager navigationManager { get; set; }

		protected Tuple<List<QuizViewModel>, List<QuestionViewModel>, List<AnswerViewModel>> quizTupleList { get; set; }

		protected QuestionViewModel currentQuestion { get; set; }
		protected long currentAnswerId { get; set; }
		protected List<long> rightAnswers { get; set; }
		protected List<long> wrongAnswers { get; set; }
		protected int totalAttemptedQuestion { get; set; }
		protected bool IsInit { get; set; } = true;
		protected bool IsSuccess { get; set; }
		protected bool isInProgress { get; set; } = true;

		protected async override Task OnInitializedAsync()
		{
			this.userStateService.OnChange += StateHasChanged;

			this.userStateService.UserId = 9;

			this.quizTupleList = await quizProvider.GetQuizListByUserIdAsync(this.userStateService.UserId);

			if (quizTupleList.Item1.Count == 1)
			{
				this.currentQuestion = this.quizTupleList.Item2[0];
			}

			this.isInProgress = false;
			await base.OnInitializedAsync();
		}

		protected void Next()
		{
			int index = this.quizTupleList.Item2.FindIndex(x => x.Id == currentQuestion.Id);

			if (this.quizTupleList.Item2.Count == index + 1)
			{
				this.currentQuestion = this.quizTupleList.Item2[0];
			}
			else
			{
				this.currentQuestion = this.quizTupleList.Item2[index + 1];
			}

			if (this.quizTupleList.Item3
				.Any(x => x.QuestionId == this.currentQuestion.Id && x.Id == this.currentAnswerId && x.IsCorrect))
			{
				if (!this.rightAnswers.Any(x => x == this.currentAnswerId))
				{
					this.rightAnswers.Add(this.currentAnswerId);
				}

				if (this.wrongAnswers.Any(x => x == this.currentAnswerId))
				{
					this.wrongAnswers.Remove(this.currentAnswerId);
				}
			}
			else if (this.quizTupleList.Item3
				.Any(x => x.QuestionId == this.currentQuestion.Id && x.Id == this.currentAnswerId && !x.IsCorrect))
			{
				if (this.rightAnswers.Any(x => x == this.currentAnswerId))
				{
					this.rightAnswers.Remove(this.currentAnswerId);
				}

				if (!this.wrongAnswers.Any(x => x == this.currentAnswerId))
				{
					this.wrongAnswers.Add(this.currentAnswerId);
				}
			}

			this.currentAnswerId = 0;
		}

		protected void Previous()
		{
			int index = this.quizTupleList.Item2.FindIndex(x => x.Id == currentQuestion.Id);

			if (index == 0)
			{
				this.currentQuestion = this.quizTupleList.Item2[this.quizTupleList.Item2.Count - 1];
			}
			else
			{
				this.currentQuestion = this.quizTupleList.Item2[index - 1];
			}

			if (this.quizTupleList.Item3
				.Any(x => x.QuestionId == this.currentQuestion.Id && x.Id == this.currentAnswerId && x.IsCorrect))
			{
				if (!this.rightAnswers.Any(x => x == this.currentAnswerId))
				{
					this.rightAnswers.Add(this.currentAnswerId);
				}

				if (this.wrongAnswers.Any(x => x == this.currentAnswerId))
				{
					this.wrongAnswers.Remove(this.currentAnswerId);
				}
			}
			else if (this.quizTupleList.Item3
				.Any(x => x.QuestionId == this.currentQuestion.Id && x.Id == this.currentAnswerId && !x.IsCorrect))
			{
				if (this.rightAnswers.Any(x => x == this.currentAnswerId))
				{
					this.rightAnswers.Remove(this.currentAnswerId);
				}

				if (!this.wrongAnswers.Any(x => x == this.currentAnswerId))
				{
					this.wrongAnswers.Add(this.currentAnswerId);
				}
			}

			this.currentAnswerId = 0;
		}

		protected void OnAnswerSelected(ChangeEventArgs e)
		{
			this.currentAnswerId = Convert.ToInt64(e.Value);
		}

		protected async Task SubmitAnswer()
		{
			//this.quizProvider.CalculateQuizResultAsync()
		}
	}

	class Answers
	{
		long QuestionId { get; set; }
		long IsCorrect { get; set; }
	}
}

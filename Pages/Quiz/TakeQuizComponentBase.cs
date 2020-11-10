using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using company_delta_flow_task_blazor.Common;
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

		protected QuestionViewModel currentQuestion { get; set; } = new QuestionViewModel();
		protected List<long> rightAnswers { get; set; } = new List<long>();
		protected List<long> wrongAnswers { get; set; } = new List<long>();
		protected List<Question> answeredQuestions { get; set; } = new List<Question>();
		protected int totalAttemptedQuestion { get; set; }
		protected int questionNumber { get; set; } = 1;
		protected bool IsInit { get; set; } = true;
		protected bool IsSuccess { get; set; }
		protected bool isCalculated { get; set; }
		protected bool isInProgress { get; set; } = true;

		protected async override Task OnInitializedAsync()
		{
			this.userStateService.OnChange += StateHasChanged;

			if (this.userStateService.UserId <= 0)
			{
				this.LogOut();
			}

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
				this.questionNumber = 1;
			}
			else
			{
				this.currentQuestion = this.quizTupleList.Item2[index + 1];
				this.questionNumber = index + 2;
			}

			this.userStateService.SelectedRadioButton = this.answeredQuestions.FirstOrDefault(x => x.Id == this.currentQuestion.Id)?.Answer.Id ?? 0;
		}

		protected void Previous()
		{
			int index = this.quizTupleList.Item2.FindIndex(x => x.Id == currentQuestion.Id);

			if (index == 0)
			{
				this.currentQuestion = this.quizTupleList.Item2[this.quizTupleList.Item2.Count - 1];
				this.questionNumber = this.quizTupleList.Item2.Count;
			}
			else
			{
				this.currentQuestion = this.quizTupleList.Item2[index - 1];
				this.questionNumber = index;
			}

			this.userStateService.SelectedRadioButton = this.answeredQuestions.FirstOrDefault(x => x.Id == this.currentQuestion.Id)?.Answer.Id ?? 0;
		}

		protected void OnAnswerSelected(AnswerViewModel e)
		{
			if (this.quizTupleList.Item3
				.Any(x => x.QuestionId == this.currentQuestion.Id && x.Id == e.Id && x.IsCorrect))
			{
				if (!this.rightAnswers.Any(x => x == this.currentQuestion.Id))
				{
					this.rightAnswers.Add(this.currentQuestion.Id);
				}

				if (this.wrongAnswers.Any(x => x == this.currentQuestion.Id))
				{
					this.wrongAnswers.Remove(this.currentQuestion.Id);
				}
			}
			else if (this.quizTupleList.Item3
				.Any(x => x.QuestionId == this.currentQuestion.Id && x.Id == e.Id && !x.IsCorrect))
			{
				if (this.rightAnswers.Any(x => x == this.currentQuestion.Id))
				{
					this.rightAnswers.Remove(this.currentQuestion.Id);
				}

				if (!this.wrongAnswers.Any(x => x == this.currentQuestion.Id))
				{
					this.wrongAnswers.Add(this.currentQuestion.Id);
				}
			}

			this.userStateService.SelectedRadioButton = e.Id;
			this.answeredQuestions.RemoveAll(x => x.Id == this.currentQuestion.Id);

			this.answeredQuestions.Add(new Question
			{
				Id = this.currentQuestion.Id,
				Answer = new Answer
				{
					Id = e.Id
				}
			});
		}

		protected async Task SubmitAnswer()
		{
			this.isInProgress = true;
			this.isCalculated = false;
			this.totalAttemptedQuestion = this.answeredQuestions.Count;

			if(this.totalAttemptedQuestion > 5)
			{
				bool isSuccess = await this.quizProvider.CalculateQuizResultAsync(new UserAnswerViewModel
				{
					QuizId = this.currentQuestion.QuizId,
					TotalAttempted = this.totalAttemptedQuestion,
					TotalCorrect = this.rightAnswers.Count,
					TotalQuestion = this.quizTupleList.Item2.Count,
					UserId = this.userStateService.UserId
				}, cancellationTokenSource.Token);

				this.isCalculated = isSuccess;
				this.isInProgress = false;
			}
		}

		protected void LogOut()
		{
			this.userStateService.UserId = 0;
			this.userStateService.SelectedRadioButton = 0;
			this.navigationManager.NavigateTo(LocalUrl.SignIn, true);
		}
	}

	public class Question
	{
		public long Id { get; set; }
		public Answer Answer { get; set; }
	}

	public class Answer
	{
		public long Id { get; set; }
	}
}

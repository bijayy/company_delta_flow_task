using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using company_delta_flow_task_blazor.Common;
using company_delta_flow_task_blazor.ViewModels;

namespace company_delta_flow_task_blazor.Data.Providers
{
	public class QuizProvider : IQuizProvider
	{
		public async Task<bool> ConfigureQuizForUserAsync(ConfigureUserQuizViewModel configureUserQuizViewModel, CancellationToken cancellationToken = default)
		{
			using (SqlConnection connection = new SqlConnection(DefaultDataConfig.ConnectionString))
			{
				int insertedRecord = 0;

				try
				{
					SqlCommand command = new SqlCommand("qsp_configureUserQuiz", connection);
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@IsExpired", configureUserQuizViewModel.IsExpired);
					command.Parameters.AddWithValue("@UserId", configureUserQuizViewModel.UserId);
					command.Parameters.AddWithValue("@QuizId", configureUserQuizViewModel.QuizId);

					connection.Open();
					insertedRecord = await command.ExecuteNonQueryAsync(cancellationToken);
					return true;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
				finally
				{
					connection.Close();
				}

				return false;
			}
		}

		public async Task<bool> CreateQuizAsync(QuizViewModel quizViewModel, CancellationToken cancellationToken = default)
		{
			using (SqlConnection connection = new SqlConnection(DefaultDataConfig.ConnectionString))
			{
				int insertedRecord = 0;

				try
				{
					SqlCommand command = new SqlCommand("qsp_createQuiz", connection);
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@Name", quizViewModel.Name);
					command.Parameters.AddWithValue("@PassPercentage", quizViewModel.PassPercentage);

					connection.Open();
					insertedRecord = await command.ExecuteNonQueryAsync(cancellationToken);
					return true;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
				finally
				{
					connection.Close();
				}

				return false;
			}
		}

		public async Task<bool> CreateQuestionAsync(QuestionViewModel questionViewModel, CancellationToken cancellationToken = default)
		{
			using (SqlConnection connection = new SqlConnection(DefaultDataConfig.ConnectionString))
			{
				int insertedRecord = 0;

				try
				{
					SqlCommand command = new SqlCommand("qsp_createQuestion", connection);
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@Name", questionViewModel.Name);
					command.Parameters.AddWithValue("@Description", questionViewModel.Description);
					command.Parameters.AddWithValue("@QuizId", questionViewModel.QuizId);

					connection.Open();
					insertedRecord = await command.ExecuteNonQueryAsync(cancellationToken);
					return true;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
				finally
				{
					connection.Close();
				}

				return false;
			}
		}

		public async Task<bool> CreateAnswerAsync(AnswerViewModel answerViewModel, CancellationToken cancellationToken = default)
		{
			using (SqlConnection connection = new SqlConnection(DefaultDataConfig.ConnectionString))
			{
				int insertedRecord = 0;

				try
				{
					SqlCommand command = new SqlCommand("qsp_createAnswer", connection);
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@Name", answerViewModel.Name);
					command.Parameters.AddWithValue("@Description", answerViewModel.Description);
					command.Parameters.AddWithValue("@IsCorrect", answerViewModel.IsCorrect);
					command.Parameters.AddWithValue("@QuestionId", answerViewModel.QuestionId);

					connection.Open();
					insertedRecord = await command.ExecuteNonQueryAsync(cancellationToken);
					return true;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
				finally
				{
					connection.Close();
				}

				return false;
			}
		}

		public async Task<Tuple<List<QuizViewModel>, List<QuestionViewModel>, List<AnswerViewModel>>> GetQuizListByUserIdAsync(long userId, CancellationToken cancellationToken = default)
		{
			var tuples = Tuple.Create(new List<QuizViewModel>(), new List<QuestionViewModel>(), new List<AnswerViewModel>());

			using (SqlConnection connection = new SqlConnection(DefaultDataConfig.ConnectionString))
			{
				try
				{
					SqlCommand command = new SqlCommand("usp_getQuizByUserId", connection);
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@UserId", userId);
					connection.Open();

					SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

					List<QuizViewModel> quizViewModels = new List<QuizViewModel>();
					var questions = new List<QuestionViewModel>();
					var answers = new List<AnswerViewModel>();

					while (await reader.ReadAsync(cancellationToken))
					{
						long quizId = Convert.ToInt64(reader["QuizId"]);
						string quizName = reader["QuizName"].ToString();
						int quizPassPercentage = Convert.ToInt32(reader["PassPercentage"]);

						if(!quizViewModels.Any(x => x.Id == quizId))
						{
							quizViewModels.Add(new QuizViewModel()
							{
								Id = quizId,
								Name = quizName,
								PassPercentage = quizPassPercentage
							});
						}

						long questionId = Convert.ToInt64(reader["QuestionId"]);
						string question = reader["Question"].ToString();
						string questionDescription = reader["QuestionDetails"].ToString();

						if (!questions.Any(y => y.Id == questionId && y.QuizId == quizId))
						{
							questions.Add(new QuestionViewModel()
							{
								Id = questionId,
								Name = quizName,
								Description = questionDescription,
								QuizId = quizId
							});
						}

						long answerId = Convert.ToInt64(reader["AnswerId"]);
						string answer = reader["Answer"].ToString();
						bool answerIsCorrect = Convert.ToBoolean(reader["IsCorrect"]);

						if (!answers.Any(y => y.Id == answerId && y.QuestionId == questionId))
						{
							answers.Add(new AnswerViewModel()
							{
								Id = answerId,
								Name = quizName,
								IsCorrect = answerIsCorrect,
								Description = questionDescription,
								QuestionId = questionId
							});
						}
					}

					tuples.Item1.AddRange(quizViewModels);
					tuples.Item2.AddRange(questions);
					tuples.Item3.AddRange(answers);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
					return null;
				}
				finally
				{
					connection.Close();
				}

				return tuples;
			}
		}

		public Task<bool> CalculateQuizResultAsync(UserAnswerViewModel userAnswerViewModel, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}
	}
}

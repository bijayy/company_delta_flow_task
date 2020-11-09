using System;
using System.Data;
using System.Data.SqlClient;
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

		public async Task<QuizViewModel> GetQuizByUserIdAsync(long userId, CancellationToken cancellationToken = default)
		{
			using (SqlConnection connection = new SqlConnection(DefaultDataConfig.ConnectionString))
			{
				QuizViewModel quizViewModel = null;

				try
				{
					SqlCommand command = new SqlCommand("usp_getQuizByUserId", connection);
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@UserId", userId);
					connection.Open();

					SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

					while (await reader.ReadAsync(cancellationToken))
					{
						quizViewModel = new QuizViewModel();
						quizViewModel.Id = Convert.ToInt64(reader["Id"]);
						quizViewModel.Name = reader["Name"].ToString();
						quizViewModel.PassPercentage = Convert.ToInt32(reader["PassPercentage"]);
						quizViewModel.QuestionViewModel = new QuestionViewModel();

						quizViewModel.QuestionViewModel.Id = Convert.ToBoolean(reader["EmailVerified"]);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
				finally
				{
					connection.Close();
				}

				if (user != null)
				{
					if (user.IsEmailVerified)
					{
						return SignInStatus.Success;
					}

					return SignInStatus.EmailNotVerified;
				}

				return SignInStatus.Faild;
			}
		}

		public Task<bool> CalculateQuizResultAsync(UserAnswerViewModel userAnswerViewModel, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}
	}
}

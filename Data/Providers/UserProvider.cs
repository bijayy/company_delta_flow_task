using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

using company_delta_flow_task_blazor.Common;
using company_delta_flow_task_blazor.ViewModels;

namespace company_delta_flow_task_blazor.Data.Providers
{
	public class UserProvider : IUserProvider
	{
		public async Task<bool> SignInAsync(SignInViewModel signInViewModel, CancellationToken cancellationToken = default)
		{
			using (SqlConnection connection = new SqlConnection(DefaultDataConfig.ConnectionString))
			{
				object record = null;

				try
				{
					SqlCommand command = new SqlCommand("usp_signInUserByUserNameAndPassword", connection);
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@Email", signInViewModel.Email);
					command.Parameters.AddWithValue("@Password", signInViewModel.Password);
					connection.Open();

					record = await command.ExecuteScalarAsync(cancellationToken);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
				finally
				{
					connection.Close();
				}

				return record != null;
			}
		}

		public async Task<bool> SignUpAsync(SignUpViewModel signUpViewModel, CancellationToken cancellationToken = default)
		{
			using (SqlConnection connection = new SqlConnection(DefaultDataConfig.ConnectionString))
			{
				int insertedRecord = 0;

				try
				{
					SqlCommand command = new SqlCommand("usp_signUpUser", connection);
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@FullName", signUpViewModel.FullName);
					command.Parameters.AddWithValue("@Email", signUpViewModel.Email);
					command.Parameters.AddWithValue("@Gender", signUpViewModel.PhoneNumber);

					connection.Open();
					insertedRecord = await command.ExecuteNonQueryAsync(cancellationToken);
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex);
				}
				finally
				{
					connection.Close();
				}

				return insertedRecord != 0;
			}
		}

		public async Task<bool> IsUserExistAsync(string email, CancellationToken cancellationToken = default)
		{
			using (SqlConnection connection = new SqlConnection(DefaultDataConfig.ConnectionString))
			{
				object record = null;

				try
				{
					SqlCommand command = new SqlCommand("usp_userExistsByEmail", connection);
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@Email", email);
					connection.Open();

					record = await command.ExecuteScalarAsync(cancellationToken);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
				finally
				{
					connection.Close();
				}

				return record != null;
			}
		}
	}
}

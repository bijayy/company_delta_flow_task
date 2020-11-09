using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

using company_delta_flow_task_blazor.Common;
using company_delta_flow_task_blazor.Services;
using company_delta_flow_task_blazor.ViewModels;

using Microsoft.AspNetCore.Http;

namespace company_delta_flow_task_blazor.Data.Providers
{
	public class UserProvider : IUserProvider
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserProvider(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<SignInStatus> SignInAsync(SignInViewModel signInViewModel, CancellationToken cancellationToken = default)
		{
			using (SqlConnection connection = new SqlConnection(DefaultDataConfig.ConnectionString))
			{
				UserViewModel user = null;

				try
				{
					SqlCommand command = new SqlCommand("usp_signInUserByUserNameAndPassword", connection);
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@Email", signInViewModel.Email);
					command.Parameters.AddWithValue("@Password", signInViewModel.Password);
					connection.Open();

					SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

					while (await reader.ReadAsync(cancellationToken))
					{
						user = new UserViewModel();
						user.FullName = reader["FullName"].ToString();
						user.Email = reader["Email"].ToString();
						user.Gender = reader["Gender"].ToString();
						user.IsEmailVerified = Convert.ToBoolean(reader["EmailVerified"]);
					}
				}
				catch (Exception ex)
				{
					user = null;
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

		public async Task<SignUpStatus> SignUpAsync(SignUpViewModel signUpViewModel, CancellationToken cancellationToken = default)
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
					command.Parameters.AddWithValue("@Gender", signUpViewModel.Gender);
					command.Parameters.AddWithValue("@Password", signUpViewModel.Password);

					connection.Open();
					insertedRecord = await command.ExecuteNonQueryAsync(cancellationToken);

					if (insertedRecord != 0)
					{
						bool emailSent = await Task.Run(() => this.SendEmail(this.GetUserByEmail(signUpViewModel.Email).Result));

						return emailSent ? SignUpStatus.Success : SignUpStatus.EmailNotSent;
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

				return SignUpStatus.Faild;
			}
		}

		public async Task<Exist> IsUserExistAsync(string email, CancellationToken cancellationToken = default)
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
					return Exist.Failed;
				}
				finally
				{
					connection.Close();
				}

				return record == null ? Exist.No : Exist.Yes;
			}
		}

		public async Task<bool> VerifyUser(long Id, CancellationToken cancellationToken = default)
		{
			using (SqlConnection connection = new SqlConnection(DefaultDataConfig.ConnectionString))
			{
				int updatedRecordCount = 0;

				try
				{
					SqlCommand command = new SqlCommand("usp_verifyUserById", connection);
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@Id", Id);
					connection.Open();

					updatedRecordCount = await command.ExecuteNonQueryAsync(cancellationToken);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
				finally
				{
					connection.Close();
				}

				return updatedRecordCount != 0;
			}
		}

		public bool SendEmail(UserViewModel user)
		{
			try
			{
				MailMessage message = new MailMessage();
				SmtpClient smtp = new SmtpClient();
				message.From = new MailAddress("office.github@gmail.com");
				message.To.Add(new MailAddress(user.Email));
				message.Subject = "Verify email to login to Quiz World";
				message.IsBodyHtml = true; //to make message body as html  
				message.Body = FormEmailBody(user);
				smtp.Port = 587;
				smtp.Host = "smtp.gmail.com"; //for gmail host  
				smtp.EnableSsl = true;
				smtp.UseDefaultCredentials = false;
				smtp.Credentials = new NetworkCredential("office.github@gmail.com", "Gmail@22");
				smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtp.Send(message);

				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return false;
			}
		}

		private string FormEmailBody(UserViewModel user)
		{
			try
			{
				string messageBody = "<font>Please click the link and verify the email: </font><br><br>";
				string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
				string htmlTableEnd = "</table>";
				string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
				string htmlHeaderRowEnd = "</tr>";
				string htmlTrStart = "<tr style=\"color:#555555;\">";
				string htmlTrEnd = "</tr>";
				string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
				string htmlTdEnd = "</td>";
				messageBody += htmlTableStart;
				messageBody += htmlHeaderRowStart;
				messageBody += htmlTdStart + "Name" + htmlTdEnd;
				messageBody += htmlTdStart + "Email" + htmlTdEnd;
				messageBody += htmlTdStart + "<a href='" + this._httpContextAccessor.HttpContext.Request.Scheme + "://" + this._httpContextAccessor.HttpContext.Request.Host.Value + "/user/verify/"+user.Id+"'<a>Verify Email" + htmlTdEnd;
				messageBody += htmlHeaderRowEnd;
				messageBody = messageBody + htmlTrStart;
				messageBody = messageBody + htmlTdStart + user.FullName + htmlTdEnd; //adding name  
				messageBody = messageBody + htmlTdStart + user.Gender + htmlTdEnd; //adding Email  
				messageBody = messageBody + htmlTrEnd;

				messageBody = messageBody + htmlTableEnd;
				return messageBody; // return HTML Table as string from this function  
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return string.Empty;
			}
		}

		public async Task<UserViewModel> GetUserByEmail(string email, CancellationToken cancellationToken = default)
		{
			UserViewModel user = null;

			using (SqlConnection connection = new SqlConnection(DefaultDataConfig.ConnectionString))
			{
				try
				{
					SqlCommand command = new SqlCommand("usp_GetUserByEmail", connection);
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@email", email);
					connection.Open();

					SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

					while (await reader.ReadAsync(cancellationToken))
					{
						user = new UserViewModel();
						user.Id = Convert.ToInt64(reader["Id"]);
						user.FullName = reader["FullName"].ToString();
						user.Email = reader["Email"].ToString();
						user.Gender = reader["Gender"].ToString();
						user.IsEmailVerified = Convert.ToBoolean(reader["EmailVerified"]);
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
			}

			return user;
		}
	}
}

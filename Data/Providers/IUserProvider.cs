using System.Threading;
using System.Threading.Tasks;
using company_delta_flow_task_blazor.Common;
using company_delta_flow_task_blazor.ViewModels;

namespace company_delta_flow_task_blazor.Data.Providers
{
	public interface IUserProvider
	{
		Task<SignInStatus> SignInAsync(SignInViewModel signInViewModel, CancellationToken cancellationToken = default);
		Task<SignUpStatus> SignUpAsync(SignUpViewModel signUpViewModel, CancellationToken cancellationToken = default);
		Task<Exist> IsUserExistAsync(string email, CancellationToken cancellationToken = default);
		Task<bool> VerifyUser(long Id, CancellationToken cancellationToken = default);
		Task<UserViewModel> GetUserByEmail(string email, CancellationToken cancellationToken = default);
		bool SendEmail(UserViewModel user);
	}
}

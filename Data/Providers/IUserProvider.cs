using System.Threading;
using System.Threading.Tasks;

using company_delta_flow_task_blazor.ViewModels;

namespace company_delta_flow_task_blazor.Data.Providers
{
	public interface IUserProvider
	{
		Task<bool> SignInAsync(SignInViewModel signInViewModel, CancellationToken cancellationToken = default);
		Task<bool> SignUpAsync(SignUpViewModel signUpViewModel, CancellationToken cancellationToken = default);
		Task<bool> IsUserExistAsync(string email, CancellationToken cancellationToken = default);
	}
}

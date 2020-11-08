using System.Threading;
using System.Threading.Tasks;

using company_delta_flow_task_blazor.Common;
using company_delta_flow_task_blazor.Data.Providers;
using company_delta_flow_task_blazor.ViewModels;

using Microsoft.AspNetCore.Components;

namespace company_delta_flow_task_blazor.Pages.Users
{
	public class SignUpComponentBase : ComponentBase
	{
		CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

		[Inject]
		protected IUserProvider userProvider { get; set; }

		[Parameter]
		public SignUpViewModel signUpViewModel { get; set; }

		[Inject]
		protected NavigationManager navigationManager { get; set; }

		protected bool IsServerError { get; set; }
		protected bool IsSuccess { get; set; }
		protected bool IsUpdateFailed { get; set; }
		protected bool IsExists { get; set; }
		protected bool IsInProgress { get; set; } = true;

		protected override Task OnInitializedAsync()
		{
			this.signUpViewModel = this.signUpViewModel ?? new SignUpViewModel();
			this.IsInProgress = false;
			return base.OnInitializedAsync();
		}

		protected async Task SignUpAsync()
		{
			this.IsExists = false;
			this.IsInProgress = true;
			this.IsUpdateFailed = false;

			if (await this.userProvider.IsUserExistAsync(signUpViewModel.Email, cancellationTokenSource.Token))
			{
				this.IsExists = true;
			}
			else
			{
				signUpViewModel = new SignUpViewModel
				{
					FullName = signUpViewModel.FullName.Trim(),
					Email = signUpViewModel.Email.ToLower().Trim(),
					PhoneNumber = signUpViewModel.PhoneNumber.Trim()
				};

				this.IsSuccess = await this.userProvider.SignUpAsync(signUpViewModel, cancellationTokenSource.Token);

				if (this.IsSuccess)
				{
					this.navigationManager.NavigateTo($"{LocalUrl.SignIn}", true);
				}
			}

			this.IsInProgress = false;
		}

		protected async Task CancelAsync()
		{
			await Task.Run(() => this.navigationManager.NavigateTo($"{LocalUrl.SignIn}", false));
		}
	}
}

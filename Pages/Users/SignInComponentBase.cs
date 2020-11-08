using System;
using System.Threading;
using System.Threading.Tasks;

using company_delta_flow_task_blazor.Common;
using company_delta_flow_task_blazor.Data.Providers;
using company_delta_flow_task_blazor.ViewModels;

using Microsoft.AspNetCore.Components;

namespace company_delta_flow_task_blazor.Pages.Users
{
	public class SignInComponentBase : ComponentBase
	{
		CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

		[Inject]
		protected IUserProvider userProvider { get; set; }

		[Inject]
		protected NavigationManager navigationManager { get; set; }

		protected SignInViewModel signInViewModel { get; set; }
		protected bool IsInit { get; set; } = true;
		protected bool IsSuccess { get; set; }
		protected bool isInProgress { get; set; } = true;

		protected async override Task OnInitializedAsync()
		{
			this.signInViewModel = new SignInViewModel();
			this.isInProgress = false;
			await base.OnInitializedAsync();
		}

		protected async Task SignInAsync()
		{
			this.isInProgress = true;
			this.IsSuccess = await this.userProvider.SignInAsync(signInViewModel, cancellationTokenSource.Token);

			if (this.IsSuccess)
			{
				this.navigationManager.NavigateTo($"{LocalUrl.TakeQuiz}", true);
			}

			this.IsInit = false;
			this.isInProgress = false;
		}

		protected async Task SignUpRedirectAsync()
		{
			await Task.Run(() => this.navigationManager.NavigateTo($"{LocalUrl.SignUp}", false));
		}
	}
}

using System.ComponentModel.DataAnnotations;

namespace company_delta_flow_task_blazor.ViewModels
{
	public class SignInViewModel
	{
		[Required]
		public string Email { get; set; } = string.Empty;

		[Required]
		public string Password { get; set; } = string.Empty;
	}
}

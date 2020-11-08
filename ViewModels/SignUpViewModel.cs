using System.ComponentModel.DataAnnotations;

namespace company_delta_flow_task_blazor.ViewModels
{
	public class SignUpViewModel
	{
		[Required]
		public string FullName { get; set; } = string.Empty;

		[Required]
		public string Email { get; set; } = string.Empty;

		public string PhoneNumber { get; set; } = string.Empty;
	}
}

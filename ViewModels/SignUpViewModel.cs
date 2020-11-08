using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace company_delta_flow_task_blazor.ViewModels
{
	public class SignUpViewModel
	{
		[Required]
		public string FullName { get; set; } = string.Empty;

		[Required]
		public string Email { get; set; } = string.Empty;

		[Required]
		public string Gender { get; set; } = Common.Gender.Male.ToString();

		public IEnumerable<string> Genders => Enum.GetNames(typeof(Common.Gender));

		[Required]
		public string Password { get; set; } = string.Empty;

		[Required]
		[CompareProperty(nameof(Password))]
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}

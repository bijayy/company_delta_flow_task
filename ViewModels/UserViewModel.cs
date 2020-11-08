namespace company_delta_flow_task_blazor.ViewModels
{
	public class UserViewModel
	{
		public long Id { get; set; }

		public string FullName { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public string Gender { get; set; } = Common.Gender.Male.ToString();

		public bool IsEmailVerified { get; set; }
	}
}

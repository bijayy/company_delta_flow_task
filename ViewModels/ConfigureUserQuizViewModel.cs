using System.ComponentModel.DataAnnotations;

namespace company_delta_flow_task_blazor.ViewModels
{
	public class ConfigureUserQuizViewModel
	{
		[Required]
		public long Id { get; set; }

		public bool IsExpired { get; set; }

		[Required]
		public long UserId { get; set; }

		[Required]
		public long QuizId { get; set; }
	}
}

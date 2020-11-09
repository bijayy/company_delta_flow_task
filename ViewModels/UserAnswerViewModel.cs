using System.ComponentModel.DataAnnotations;

namespace company_delta_flow_task_blazor.ViewModels
{
	public class UserAnswerViewModel
	{
		[Required]
		public long Id { get; set; }

		[Required]
		public int TotalQuestion { get; set; }

		public int TotalAttempted { get; set; }

		public int TotalCorrect { get; set; }

		[Required]
		public long UserId { get; set; }

		[Required]
		public long QuizId { get; set; }
	}
}

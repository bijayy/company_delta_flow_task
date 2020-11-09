using System.ComponentModel.DataAnnotations;

namespace company_delta_flow_task_blazor.ViewModels
{
	public class AnswerViewModel
	{
		[Required]
		public long Id { get; set; }

		[Required]
		public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public bool IsCorrect { get; set; }

		[Required]
		public long QuestionId { get; set; }
	}
}

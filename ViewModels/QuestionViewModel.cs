using System.ComponentModel.DataAnnotations;

namespace company_delta_flow_task_blazor.ViewModels
{
	public class QuestionViewModel
	{
		[Required]
		public long Id { get; set; }

		[Required]
		public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		[Required]
		public long QuizId { get; set; }

		public AnswerViewModel AnswerViewModel { get; set; }
	}
}

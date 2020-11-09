using System.ComponentModel.DataAnnotations;

namespace company_delta_flow_task_blazor.ViewModels
{
	public class QuizViewModel
	{
		[Required]
		public long Id { get; set; }

		[Required]
		public string Name { get; set; } = string.Empty;

		public int PassPercentage { get; set; }

		public QuestionViewModel QuestionViewModel { get; set; }
	}
}

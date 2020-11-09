using System;

namespace company_delta_flow_task_blazor.Services
{
	public class UserStateService
	{
		public long UserId { get; set; }
		public long SelectedRadioButton { get; set; }

		public event Action OnChange;

		public void NotifyStateChanged() => this.OnChange?.Invoke();
	}
}

namespace PTC.Domain.Models;

public partial class TasksSaving
{
		public int TaskId { get; set; }
		public string TaskName { get; set; }
		public string TaskHour { get; set; }
		public DateTime TaskDate { get; set; }
		public DateTime TaskOpening { get; set; }
		public DateTime TaskClosing { get; set; }
}

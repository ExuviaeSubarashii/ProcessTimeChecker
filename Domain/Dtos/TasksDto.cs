namespace PTC.Domain.Dtos
{
	public class TasksDto
	{
		public required string TaskName { get; set; }
		public required string TaskHour { get; set; }
		public DateTime TaskCurrentTime { get; set; }
		public DateTime TaskOpening { get; set; }
	}
}

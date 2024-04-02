namespace PTC.Domain.Dtos
{
    public class TasksDto
    {
        public required string ProcessName { get; set; }
        public required string MachineName { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public required string FullTime { get; set; }
    }
}

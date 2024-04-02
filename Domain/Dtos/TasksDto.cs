namespace PTC.Domain.Dtos
{
    public class TasksDto
    {
        public string TaskName { get; set; } = null!;
        public int TaskHour { get; set; }
        public DateTime TaskDate { get; set; }
        public DateTime TaskOpening { get; set; }
        public DateTime TaskClosing { get; set; }
    }
}

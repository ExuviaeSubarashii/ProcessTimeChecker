namespace PTC.Domain.Models;

public partial class Tasks
{
    public int TaskId { get; set; }

    public string TaskName { get; set; } = null!;

    public int TaskHour { get; set; }

    public DateTime TaskDate { get; set; }

    public DateTime TaskOpening { get; set; }
    public DateTime TaskClosing { get; set; }
}

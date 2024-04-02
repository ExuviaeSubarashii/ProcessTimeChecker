namespace PTC.Domain.Models;

public partial class TasksSaving
{
    public int TaskId { get; set; }
    public string TaskName { get; set; }
    public string TaskHour { get; set; }
    public string TaskDate { get; set; }
    public string TaskOpening { get; set; }
    public string TaskClosing { get; set; }
}

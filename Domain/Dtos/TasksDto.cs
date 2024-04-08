namespace PTC.Domain.Dtos
{
   public class TasksDto
   {
	  public string TaskName { get; set; } = null!;
	  public string TaskHour { get; set; }
	  public DateTime TaskDate { get; set; }
	  public DateTime TaskOpening { get; set; }
   }
}

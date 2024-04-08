using PTC.Domain.Dtos;
using PTC.Services.Services;
using System.Timers;

namespace ProcessConsole
{
   internal class Program
   {
	  private static System.Timers.Timer fetchTimer;
	  private readonly ProcessServices _PS;

	  ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
	  static void Main(string[] args)
	  {
		 fetchTimer = new System.Timers.Timer(5000);
		 fetchTimer.Elapsed += OnTimedEvent;
		 fetchTimer.AutoReset = true;
		 fetchTimer.Enabled = true;
		 Console.ReadKey();
	  }
	  private async static void OnTimedEvent(Object source, ElapsedEventArgs e)
	  {
		 Console.Clear();
		 await WriteProcessAsync();
	  }
	  private static async Task WriteProcessAsync()
	  {

		 ProcessServices processServices = new ProcessServices();
		 //"Code", 
		 List<TasksDto> tasksDtos = (List<TasksDto>)await processServices.GetTheProcesses();
		 foreach (TasksDto task in tasksDtos)
		 {
			Console.WriteLine($"\x1b[31mProcess Name: {task.TaskName}\x1b[0m");
			Console.WriteLine($"Start Time: {task.TaskOpening}");
			Console.WriteLine($"End Time: {task.TaskClosing}");
			Console.WriteLine(task.TaskDate);
			Console.WriteLine(task.TaskHour);
			Console.WriteLine();
		 }
	  }
   }
}
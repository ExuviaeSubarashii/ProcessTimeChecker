using Microsoft.EntityFrameworkCore;
using PTC.Domain.Dtos;
using PTC.Domain.Interfaces;
using PTC.Domain.Models;
using System.Diagnostics;

namespace PTC.Services.Services
{
   public class ProcessServices : ILookForProcessInterface
   {
	  public async Task<IEnumerable<TasksDto>> GetTheProcesses()
	  {
		 string[] processName = new string[] { "Code", "devenv", "Spotify", "notepad++", "Discord", "chrome" };

		 List<TasksDto> tasks = new();

		 foreach (var item in processName)
		 {
			Process? localbyname = Process.GetProcessesByName(item).FirstOrDefault();

			if (localbyname != null)
			{
			   TasksDto dto = new TasksDto
			   {
				  TaskName = localbyname.ProcessName,
				  TaskOpening = localbyname.StartTime,
				  TaskHour = FormatTimeSpan(localbyname.StartTime),
				  TaskDate = DateTime.Now
			   };
			   tasks.Add(dto);
			}
		 }
		 return tasks.ToList();
	  }
	  private static string FormatTimeSpan(DateTime timeSpan)
	  {
		 TimeSpan timeDifference = DateTime.Now - timeSpan;
		 int totalHours = (int)timeDifference.TotalHours;
		 int minutes = timeDifference.Minutes;
		 return $"{totalHours}:{minutes}";
	  }
	  public async Task SaveTaskInformation(List<TasksDto> task)
	  {
		 try
		 {
			foreach (var item in task)
			{
			   using (ProcessTimersContext context = new())
			   {
				  var doesTaskAlreadyExists = await context.TasksSaving.FirstOrDefaultAsync(x => x.TaskName == item.TaskName);

				  TasksSaving tasks = new()
				  {
					 TaskOpening = item.TaskOpening,
					 TaskDate = item.TaskDate,
					 TaskHour = FormatTimeSpan(item.TaskOpening),
					 TaskName = item.TaskName.ToString(),
				  };
				  if (doesTaskAlreadyExists == null)
				  {
					 await context.TasksSaving.AddRangeAsync(tasks);
					 await context.SaveChangesAsync();
				  }
				  else
				  {
					 doesTaskAlreadyExists.TaskHour = FormatTimeSpan(doesTaskAlreadyExists.TaskOpening);
					 await context.SaveChangesAsync();
				  }
			   }
			}
		 }
		 catch (Exception)
		 {
			throw;
		 }
	  }
   }
}

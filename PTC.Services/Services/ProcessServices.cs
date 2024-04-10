using Microsoft.EntityFrameworkCore;
using PTC.Domain.Dtos;
using PTC.Domain.Interfaces;
using PTC.Domain.Models;
using System.Diagnostics;

namespace PTC.Services.Services
{
	public class ProcessServices() : ILookForProcessInterface
	{
		public async Task<List<TasksDto>> GetTheProcesses()
		{
			List<TasksDto> tasks = new();

			using (ProcessTimersContext context = new())
			{

				List<NewTaskNames> processName = await context.NewTaskNames.ToListAsync();


				foreach (var item in processName)
				{
					Process? localbyname = Process.GetProcessesByName(item.ApplicationName).FirstOrDefault();

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
		public async Task SaveTaskInformation(string taskName)
		{
			try
			{
				using (ProcessTimersContext context = new())
				{
					var doesTaskAlreadyExists = await context.NewTaskNames.FirstOrDefaultAsync(x => x.ApplicationName == taskName);
					NewTaskNames newTask = new NewTaskNames
					{
						ApplicationName = taskName
					};
					if (doesTaskAlreadyExists == null)
					{
						await context.NewTaskNames.AddAsync(newTask);
						await context.SaveChangesAsync();
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<List<CurrentlyAddedTasksDto>> GetCurrentlyAddedTasks()
		{
			using (ProcessTimersContext context = new())
			{
				var currentTasks = await context.NewTaskNames.Select(x => new CurrentlyAddedTasksDto
				{
					TaskName = x.ApplicationName,
					Id = x.Id,
				}).ToListAsync();
				if (currentTasks.Any())
				{
					return currentTasks;
				}
				else
				{
					return new List<CurrentlyAddedTasksDto>();
				}
			}
		}
		public async Task DeleteTask(string taskName)
		{
			using (ProcessTimersContext context = new())
			{
				var doesTaskExist = await context.NewTaskNames.FirstOrDefaultAsync(task => task.ApplicationName == taskName);
				if (doesTaskExist == null)
				{

				}
				else
				{
					context.NewTaskNames.Remove(doesTaskExist);
					await context.SaveChangesAsync();
				}
			}
		}
	}
}

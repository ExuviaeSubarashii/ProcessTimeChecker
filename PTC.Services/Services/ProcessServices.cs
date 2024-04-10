using Microsoft.EntityFrameworkCore;
using PTC.Domain.Dtos;
using PTC.Domain.Interfaces;
using PTC.Domain.Models;
using System.Diagnostics;

namespace PTC.Services.Services
{
	public class ProcessServices() : ILookForProcessInterface
	{
		public static string desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		public static string relativePath = @"ProcessTimeChecker\PTC.Resources\TaskNames.txt";
		public static string filePath = Path.Combine(desktopDirectory, relativePath);

		public async Task<List<TasksDto>> GetTheProcessesByContext()
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
		public async Task SaveTaskInformationByContext(string taskName)
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
		public async Task<List<CurrentlyAddedTasksDto>> GetCurrentlyAddedTasksByContext()
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
		public async Task DeleteTaskByContext(string taskName)
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
		public async Task<List<TasksDto>> GetTheProcesses()
		{
			List<TasksDto> tasks = new();
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException($"File not found: {filePath}");
			}
			string processName = await File.ReadAllTextAsync(filePath);
			string[] processNames = processName.Split(',').ToArray();
			foreach (var item in processNames)
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
		public async Task SaveTaskInformation(string taskName)
		{
			try
			{
				string processName = File.ReadAllText(filePath);
				List<string> processNames = processName.Split(',').ToList();
				bool? doesExists = processNames.Any(x => x.Contains(taskName));
				if (doesExists == true)
				{

				}
				else
				{
					processNames.Add(taskName);
					File.Create(filePath).Close();
					await using (StreamWriter outputFile = new(filePath, true))
					{
						outputFile.Write(string.Join(",", processNames));
					}
				}

			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<List<string>> GetCurrentlyAddedTasks()
		{
			string processName = await File.ReadAllTextAsync(filePath);
			List<string> processNames = processName.Split(',').ToList();

			if (processNames.Any())
			{
				return processNames;
			}
			else
			{
				return new List<string>();
			}

		}
		public async Task DeleteTask(string taskName)
		{
			string processName = File.ReadAllText(filePath);
			List<string> processNames = processName.Split(',').ToList();
			bool doesExists = processNames.Any(x => x.Contains(taskName));
			if (doesExists)
			{
				processNames.Remove(taskName);
				File.Create(filePath).Close();
				await using (StreamWriter outputFile = new(filePath, true))
				{
					outputFile.Write(string.Join(",", processNames));

				}
			}
		}
	}
}

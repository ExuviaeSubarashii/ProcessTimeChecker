using PTC.Domain.Dtos;
using PTC.Domain.Interfaces;
using System.Diagnostics;

namespace PTC.Services.Services
{
	public class ProcessServices() : ILookForProcessInterface
	{
		private readonly static string _desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		private readonly static string _relativePath = @"ProcessTimeChecker\PTC.Resources\TaskNames.txt";
		private readonly static string _filePath = Path.Combine(_desktopDirectory, _relativePath);

		public async Task<List<TasksDto>> GetTheProcesses()
		{
			List<TasksDto> tasks = new();

			if (await FileService.CheckIfFileExists())
			{

				string processName = await File.ReadAllTextAsync(_filePath);
				List<string> processNames = processName.Split(',').ToList();
				processNames.RemoveAll(x => x == "");
				if (processNames.Count > 0)
				{

					foreach (var item in processNames)
					{
						Process? localbyname = Process.GetProcessesByName(item).FirstOrDefault();

						if (localbyname is { })
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
			else
			{
				return tasks;
			}

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
				string processName = File.ReadAllText(_filePath);
				List<string> processNames = processName.Split(',').ToList();
				processNames.RemoveAll(x => x == "");
				if (processNames.Count == 0)
				{
					processNames.Add(taskName);
					File.Create(_filePath).Close();
					await using (StreamWriter outputFile = new(_filePath, true))
					{
						outputFile.Write(taskName);
					}
				}
				else
				{
					processNames.Add(taskName);
					File.Create(_filePath).Close();
					await using (StreamWriter outputFile = new(_filePath, true))
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
			string processName = await File.ReadAllTextAsync(_filePath);
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
			string processName = File.ReadAllText(_filePath);
			List<string> processNames = processName.Split(',').ToList();
			bool doesExists = processNames.Any(x => x.Contains(taskName));
			if (doesExists)
			{
				processNames.Remove(taskName);
				File.Create(_filePath).Close();
				await using (StreamWriter outputFile = new(_filePath, true))
				{
					outputFile.Write(string.Join(",", processNames));

				}
			}
		}
	}
}

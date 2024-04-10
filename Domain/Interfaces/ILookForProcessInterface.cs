using PTC.Domain.Dtos;

namespace PTC.Domain.Interfaces
{
	public interface ILookForProcessInterface
	{
		Task<List<TasksDto>> GetTheProcesses();
		Task SaveTaskInformation(string taskName);
		Task<List<string>> GetCurrentlyAddedTasks();
		Task DeleteTask(string taskName);
		Task SaveTaskInformationByContext(string taskName);
		Task<List<TasksDto>> GetTheProcessesByContext();
		Task<List<CurrentlyAddedTasksDto>> GetCurrentlyAddedTasksByContext();
		Task DeleteTaskByContext(string taskName);
	}
}

using PTC.Domain.Dtos;

namespace PTC.Domain.Interfaces
{
	public interface ILookForProcessInterface
	{
		Task<List<TasksDto>> GetTheProcesses();
		Task SaveTaskInformation(string taskName);
		Task<List<CurrentlyAddedTasksDto>> GetCurrentlyAddedTasks();
	}
}

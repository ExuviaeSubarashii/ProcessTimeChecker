using PTC.Domain.Dtos;

namespace PTC.Domain.Interfaces
{
   public interface ILookForProcessInterface
   {
	 public Task<List<TasksDto>> GetTheProcesses();
	 public Task SaveTaskInformation(string taskName);
	 public Task<List<string>> GetCurrentlyAddedTasks();
	 public Task DeleteTask(string taskName);
	 public Task<bool> CreateTaskNamesFileIfDoesntExist();
	 public Task BulkDeleteTasks(List<TasksDto> taskList, CancellationToken cancellationToken);
   }
}

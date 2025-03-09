using PTC.Domain.Dtos;

namespace PTC.Domain.Interfaces
{
   public interface ILookForProcessInterface
   {
	 public Task<List<TasksDto>> GetTheProcesses(CancellationToken cancellationToken);
	 public Task SaveTaskInformation(string taskName, CancellationToken cancellationToken);
	 public Task<List<string>> GetCurrentlyAddedTasks(CancellationToken cancellationToken);
	 public Task DeleteTask(string taskName, CancellationToken cancellationToken);
	 public Task<bool> CreateTaskNamesFileIfDoesntExist(CancellationToken cancellationToken);
	 public Task BulkDeleteTasks(List<TasksDto> taskList, CancellationToken cancellationToken);
   }
}

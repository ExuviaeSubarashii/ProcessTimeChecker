using PTC.Domain.Dtos;

namespace PTC.Domain.Interfaces
{
	public interface ILookForProcessInterface
	{
		Task<IEnumerable<TasksDto>> GetTheProcesses();
		Task SaveTaskInformation(List<TasksDto> task);
	}
}

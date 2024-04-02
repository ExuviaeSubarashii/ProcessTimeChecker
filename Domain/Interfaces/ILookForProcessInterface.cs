using PTC.Domain.Dtos;

namespace PTC.Domain.Interfaces
{
    public interface ILookForProcessInterface
    {
        List<TasksDto> GetTheProcesses(string[]? processName);
        Task<string> SaveTaskInformation(List<TasksDto> task);
    }
}

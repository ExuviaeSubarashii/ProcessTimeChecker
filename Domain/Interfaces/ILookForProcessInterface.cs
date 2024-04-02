using PTC.Domain.Dtos;
using PTC.Domain.Models;

namespace PTC.Domain.Interfaces
{
    public interface ILookForProcessInterface
    {
        List<TasksDto> GetTheProcesses(string[]? processName);
        Task<string> SaveTaskInformation(List<Tasks> task);
    }
}

using PTC.Domain.Dtos;

namespace PTC.Domain.Interfaces
{
   public interface IWorkSpaceInterface
   {
	 public Task CreateNewWorkSpace(WorkSpaceDto workSpaceDto);
	 public Task RemoveWorkSpace(WorkSpaceDto workSpaceDto);
	 public Task UpdateWorkSpace(WorkSpaceDto workSpaceDto);
	 public Task<List<WorkSpaceDto>> GetAllWorkSpaces();
	 public Task<WorkSpaceDto> GetWorkSpaceById(Guid id);
	 public Task<bool> CreateWorkSpaceFileIfDoesntExists();
   }
}

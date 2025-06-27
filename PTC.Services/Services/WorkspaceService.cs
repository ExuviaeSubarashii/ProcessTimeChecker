using PTC.Domain.Dtos;
using PTC.Domain.GlobalClasses;
using PTC.Domain.Interfaces;
using System.Text.Json;

namespace PTC.Services.Services
{
   public class WorkspaceService : IWorkSpaceInterface
   {
	 public async Task CreateNewWorkSpace(WorkSpaceDto workSpaceDto)
	 {
	    var workspaces = await GetAllWorkSpaces();
	    workspaces.Add(workSpaceDto);
	    var jsonString = JsonSerializer.Serialize(workspaces, new JsonSerializerOptions { WriteIndented = true });
	    await File.WriteAllTextAsync(GlobalVariables._workspacesFilePath, jsonString);
	 }

	 public async Task<bool> CreateWorkSpaceFileIfDoesntExists()
	 {
	    if (!File.Exists(GlobalVariables._workspacesFilePath))
	    {
		  var workspaces = new List<WorkSpaceDto>
		  {
			    new WorkSpaceDto
			    {
				   WorkSpaceDescription = "Default Workspace",
				   WorkSpaceId = Guid.NewGuid(),
				   WorkSpaceName = "Default Workspace",
				   WorkSpaceItems = ["PTC.exe"]
			    }
			};
		  string jsonString = JsonSerializer.Serialize(workspaces, new JsonSerializerOptions { WriteIndented = true });
		  await File.WriteAllTextAsync(GlobalVariables._workspacesFilePath, jsonString);
		  return true;
	    }
	    return false;
	 }

	 public async Task<List<WorkSpaceDto>> GetAllWorkSpaces()
	 {
	    if (await CreateWorkSpaceFileIfDoesntExists() == false)
	    {
		  string jsonString = await File.ReadAllTextAsync(GlobalVariables._workspacesFilePath);
		  var data = JsonSerializer.Deserialize<List<WorkSpaceDto>>(jsonString);
		  return data ?? new List<WorkSpaceDto>();
	    }
	    else
	    {
		  return new List<WorkSpaceDto> { new WorkSpaceDto() };
	    }
	 }

	 public async Task<WorkSpaceDto> GetWorkSpaceById(Guid id)
	 {
	    var selectedWorkSpace = await GetAllWorkSpaces();
	    var currentWorkSpace = selectedWorkSpace.FirstOrDefault(x => x.WorkSpaceId == id);
	    if (currentWorkSpace is null)
	    {
		  throw new Exception("WorkSpace not found");
	    }
	    return currentWorkSpace;
	 }

	 public async Task RemoveWorkSpace(WorkSpaceDto workSpaceDto)
	 {
	    var allWorkSpaces = await GetAllWorkSpaces();
	    var existingWorkSpace = allWorkSpaces.FirstOrDefault(ws => ws.WorkSpaceId == workSpaceDto.WorkSpaceId);
	    allWorkSpaces.Remove(existingWorkSpace);
	    string jsonString = JsonSerializer.Serialize(allWorkSpaces, new JsonSerializerOptions { WriteIndented = true });
	    await File.WriteAllTextAsync(GlobalVariables._workspacesFilePath, jsonString);
	 }

	 public async Task UpdateWorkSpace(WorkSpaceDto workSpaceDto)
	 {
	    var allWorkSpaces = await GetAllWorkSpaces();
	    var existingWorkSpace = allWorkSpaces.FirstOrDefault(ws => ws.WorkSpaceId == workSpaceDto.WorkSpaceId);
	    allWorkSpaces.Remove(existingWorkSpace);

	    existingWorkSpace.WorkSpaceName = workSpaceDto.WorkSpaceName;
	    existingWorkSpace.WorkSpaceDescription = workSpaceDto.WorkSpaceDescription;
	    existingWorkSpace.WorkSpaceItems = workSpaceDto.WorkSpaceItems;


	    allWorkSpaces.Add(existingWorkSpace);

	    string jsonString = JsonSerializer.Serialize(allWorkSpaces, new JsonSerializerOptions { WriteIndented = true });
	    await File.WriteAllTextAsync(GlobalVariables._workspacesFilePath, jsonString);
	 }
   }
}

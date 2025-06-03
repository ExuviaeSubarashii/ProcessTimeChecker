namespace PTC.Domain.Dtos
{
   public class WorkSpaceDto
   {
	 public Guid? WorkSpaceId { get; set; }
	 public string? WorkSpaceName { get; set; } = string.Empty;
	 public string? WorkSpaceDescription { get; set; } = string.Empty;
	 public List<string?> WorkSpaceItems { get; set; } = new List<string?>();
	 public string WorkSpaceItemsDisplay => string.Join(", ", WorkSpaceItems);
   }
}

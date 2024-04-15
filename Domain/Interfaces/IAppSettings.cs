namespace PTC.Domain.Interfaces
{
	public interface IAppSettings
	{
		public Task<bool> IsTopMostAsync();
		public Task ChangeTopMostPropertyAsync();
		public Task<bool> CreateSettingsFileIfDoesntExistsAsync();
	}
}

namespace PTC.Domain.Interfaces
{
	public interface IAppSettings
	{
		public Task<bool> IsTopMostAsync();
		public Task ChangeTopMostPropertyAsync();
		public Task<bool> CreateSettingsFileIfDoesntExistsAsync();
		public Task ChangeThemeAsync();
		Task<string> WhatThemeIsIt();
		Task<int> GetRefreshTime();
		Task SetRefreshTime(int refreshTime);
		Task SetLanguage();
		Task<string> GetLanguage();
	}
}

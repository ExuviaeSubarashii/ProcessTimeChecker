namespace PTC.Domain.Interfaces
{
	public interface IAppSettings
	{
		Task<bool> IsTopMostAsync();
		Task ChangeTopMostPropertyAsync();
		Task<bool> CreateSettingsFileIfDoesntExistsAsync();
		Task ChangeThemeAsync();
		Task<string> WhatThemeIsIt();
		Task<int> GetRefreshTime();
		Task SetRefreshTime(int refreshTime);
		Task SetLanguage();
		Task<string> GetLanguage();

	}
}

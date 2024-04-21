using PTC.Domain.Interfaces;
using System.Text.Json;

namespace PTC.Services.Services
{
	public class SettingsService : IAppSettings
	{
		private readonly static string desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		private readonly static string relativePath = @"ProcessTimeChecker\PTC.Resources\settings.json";
		private readonly static string filePath = Path.Combine(desktopDirectory, relativePath);
		public class Settings
		{
			public bool TopMost { get; set; }
			public string CurrentTheme { get; set; } = null!;
			public int RefreshTime { get; set; }
		}
		private async Task<Settings> ReadAllDataAsync()
		{
			string jsonString = await File.ReadAllTextAsync(filePath);
			var data = JsonSerializer.Deserialize<Settings>(jsonString);
			return data;
		}
		public async Task<bool> IsTopMostAsync()
		{
			if (await CreateSettingsFileIfDoesntExistsAsync() == false)
			{
				var data = await ReadAllDataAsync();
				bool isdata = data.TopMost;
				return isdata;
			}
			return false;
		}
		public async Task ChangeTopMostPropertyAsync()
		{
			if (await CreateSettingsFileIfDoesntExistsAsync() == false)
			{
				var jsonObject = await ReadAllDataAsync();
				jsonObject.TopMost = !jsonObject.TopMost;
				string updatedJson = JsonSerializer.Serialize(jsonObject);
				await File.WriteAllTextAsync(filePath, updatedJson);
			}

		}
		public async Task<bool> CreateSettingsFileIfDoesntExistsAsync()
		{
			if (!File.Exists(filePath))
			{
				var settings = new Settings
				{
					TopMost = false,
					CurrentTheme = "Dark",
					RefreshTime = 2
				};
				string jsonString = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
				await File.WriteAllTextAsync(filePath, jsonString);
				return true;
			}
			return false;
		}
		public async Task ChangeThemeAsync()
		{
			if (await CreateSettingsFileIfDoesntExistsAsync() == false)
			{
				var jsonObject = await ReadAllDataAsync();
				if (jsonObject.CurrentTheme == "Dark")
				{
					jsonObject.CurrentTheme = "Light";
				}
				else if (jsonObject.CurrentTheme == "Light")
				{
					jsonObject.CurrentTheme = "Dark";
				}
				string updatedJson = JsonSerializer.Serialize(jsonObject);
				await File.WriteAllTextAsync(filePath, updatedJson);
			}
		}
		public async Task<string> WhatThemeIsIt()
		{
			if (await CreateSettingsFileIfDoesntExistsAsync() == false)
			{

				var data = await ReadAllDataAsync();
				var whatTheme = data.CurrentTheme;
				return whatTheme;
			}
			return "Dark";
		}
		public async Task<int> GetRefreshTime()
		{
			if (await CreateSettingsFileIfDoesntExistsAsync() == false)
			{

				var data = await ReadAllDataAsync();
				return data.RefreshTime;
			}
			return 2;
		}
		public async Task SetRefreshTime(int refreshTime)
		{
			if (await CreateSettingsFileIfDoesntExistsAsync() == false)
			{
				var data = await ReadAllDataAsync();
				data.RefreshTime = refreshTime;
				string updatedJson = JsonSerializer.Serialize(data);
				await File.WriteAllTextAsync(filePath, updatedJson);
			}
		}
	}
}

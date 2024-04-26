using PTC.Domain.GlobalClasses;
using PTC.Domain.Interfaces;
using System.Text.Json;

namespace PTC.Services.Services
{
	public class SettingsService : IAppSettings
	{
		public class Settings
		{
			public bool TopMost { get; set; }
			public string CurrentTheme { get; set; } = null!;
			public int RefreshTime { get; set; }
			public string Language { get; set; } = null!;
		}
		private async Task<Settings> ReadAllDataAsync()
		{
			if (await CreateSettingsFileIfDoesntExistsAsync() == false)
			{
				string jsonString = await File.ReadAllTextAsync(GlobalVariables._settingsFilePath);
				var data = JsonSerializer.Deserialize<Settings>(jsonString);
				return data;
			}
			else
			{
				Settings settings = new()
				{
					CurrentTheme = "Light",
					RefreshTime = 2,
					Language = "Turkish",
					TopMost = false
				};
				return settings;
			}

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
				await File.WriteAllTextAsync(GlobalVariables._settingsFilePath, updatedJson);
			}

		}
		public async Task<bool> CreateSettingsFileIfDoesntExistsAsync()
		{
			if (!File.Exists(GlobalVariables._settingsFilePath))
			{
				var settings = new Settings
				{
					TopMost = false,
					CurrentTheme = "Dark",
					RefreshTime = 2,
					Language = "Turkish"
				};
				string jsonString = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
				await File.WriteAllTextAsync(GlobalVariables._settingsFilePath, jsonString);
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
				await File.WriteAllTextAsync(GlobalVariables._settingsFilePath, updatedJson);
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
				await File.WriteAllTextAsync(GlobalVariables._settingsFilePath, updatedJson);
			}
		}
		public async Task SetLanguage()
		{
			if (await CreateSettingsFileIfDoesntExistsAsync() == false)
			{
				var data = await ReadAllDataAsync();
				string currentLang = data.Language == "Turkish" ? "English" : "Turkish";
				data.Language = currentLang;
				string updatedJson = JsonSerializer.Serialize(data);
				await File.WriteAllTextAsync(GlobalVariables._settingsFilePath, updatedJson);
			}
		}
		public async Task<string> GetLanguage()
		{
			var data = await ReadAllDataAsync();
			return data.Language;
		}
	}
}

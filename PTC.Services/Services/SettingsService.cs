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
			public required bool TopMost { get; set; }
		}
		public async Task<bool> IsTopMostAsync()
		{
			if (await CreateSettingsFileIfDoesntExistsAsync() == false)
			{

				string jsonString = await Task.Run(() => File.ReadAllText(filePath));
				var data = JsonSerializer.Deserialize<Settings>(jsonString);
				bool isdata = data.TopMost;
				return isdata;
			}
			return false;
		}

		public async Task ChangeTopMostPropertyAsync()
		{
			if (await CreateSettingsFileIfDoesntExistsAsync() == false)
			{
				string jsonString = await File.ReadAllTextAsync(filePath);
				var jsonObject = JsonSerializer.Deserialize<Settings>(jsonString);
				jsonObject.TopMost = !jsonObject.TopMost;
				string updatedJson = JsonSerializer.Serialize(jsonObject);
				await File.WriteAllTextAsync(filePath, updatedJson);
			}

		}

		public async Task<bool> CreateSettingsFileIfDoesntExistsAsync()
		{
			if (!File.Exists(filePath))
			{
				var settings = new Settings { TopMost = false };
				string jsonString = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
				await File.WriteAllTextAsync(filePath, jsonString);
				return true;
			}
			return false;
		}
	}
}

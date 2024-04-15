using System.Text;

namespace PTC.Services.Services
{
	public static class FileService
	{
		private readonly static string desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		private readonly static string relativePath = @"ProcessTimeChecker\PTC.Resources\TaskNames.txt";
		private readonly static string filePath = Path.Combine(desktopDirectory, relativePath);
		public static async Task<bool> CheckIfFileExists()
		{
			if (await Task.Run(() => !File.Exists(filePath)))
			{
				StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);
				sw.Close();
				return false;
			}
			return true;
		}
		public static async Task ClearFileContentAsync()
		{
			if (await FileService.CheckIfFileExists())
			{
				await File.WriteAllTextAsync(filePath, string.Empty);
			}
		}
	}
}

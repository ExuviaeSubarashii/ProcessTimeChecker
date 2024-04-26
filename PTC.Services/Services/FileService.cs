using PTC.Domain.GlobalClasses;
using System.Text;

namespace PTC.Services.Services
{
	public static class FileService
	{
		public static async Task<bool> CheckIfFileExistsAndCreate()
		{
			if (await Task.Run(() => !File.Exists(GlobalVariables._txtFilePath)))
			{
				StreamWriter sw = new StreamWriter(GlobalVariables._txtFilePath, false, Encoding.UTF8);
				sw.Close();
				return false;
			}
			return true;
		}
		public static async Task ClearFileContentAsync()
		{
			if (await FileService.CheckIfFileExistsAndCreate())
			{
				await File.WriteAllTextAsync(GlobalVariables._txtFilePath, string.Empty);
			}
		}
	}
}

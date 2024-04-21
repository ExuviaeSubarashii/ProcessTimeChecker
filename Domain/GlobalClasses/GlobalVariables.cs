namespace PTC.Domain.GlobalClasses
{
	public static class GlobalVariables
	{
		public static string desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		public static string relativePath = @"ProcessTimeChecker\ProcessTimeCheckerWPF\bin\Debug\net8.0-windows\ProcessTimeCheckerWPF.exe";
		public static string filePath = Path.Combine(desktopDirectory, relativePath);
	}
}

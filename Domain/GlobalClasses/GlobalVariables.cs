﻿using System.Diagnostics;

namespace PTC.Domain.GlobalClasses
{
	public static class GlobalVariables
	{
		public static string currentExecutablePath = Process.GetCurrentProcess().MainModule.FileName;
		public static readonly string _desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		public static readonly string _relativePath = @"ProcessTimeChecker\PTC.Resources\TaskNames.txt";
		public static readonly string _txtFilePath = Path.Combine(_desktopDirectory, _relativePath);
	}
}

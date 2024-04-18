using PTC.Domain.Dtos;
using PTC.Services.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace ProcessTimeCheckerWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private DispatcherTimer myTimer = new DispatcherTimer();

		public List<TasksDto> tasksDtos = new();
		private readonly ProcessServices _PS = new();
		private readonly SettingsService _SS = new();

		public MainWindow()
		{
			InitializeComponent();
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			await UpdateTopMost();
			await SetCurrentTheme();
			myTimer.Tick += new EventHandler(TimerEventProcessor);
			myTimer.Interval = TimeSpan.FromSeconds(2);
			myTimer.Start();
		}
		private async void TimerEventProcessor(object? sender, EventArgs e)
		{
			taskDataGrid.ItemsSource = new List<TasksDto>();
			tasksDtos = (List<TasksDto>)await _PS.GetTheProcesses();
			taskDataGrid.ItemsSource = tasksDtos;
		}
		private async Task UpdateTopMost()
		{
			bool isTopMost = await Task.Run(() => _SS.IsTopMostAsync());
			this.Topmost = isTopMost;
		}

		private async void StayOnTop_Click(object sender, RoutedEventArgs e)
		{
			await _SS.ChangeTopMostPropertyAsync();
			await UpdateTopMost();
		}

		private void AddNewApp_Click(object sender, RoutedEventArgs e)
		{
			AddNewApp newApp = new AddNewApp();
			newApp.ShowDialog();
		}

		private async void ChangeTheme_Click(object sender, RoutedEventArgs e)
		{

			string msg = $"Temayı Değiştirmek İstiyor Musunuz? Bu Uygulamayı Yeniden Başlatacaktır. Eğer Yeniden Başlatmazsanız Uygulamayı Sonraki Açışınızda Tema Değişikliği Uygulanacaktır. / Would You Like to Change the Theme?? This will RESTART the app. If You Don't Choose To Restart the App, the Theme Change Will be Applied on Your Next Launch.";
			string title = "Change Theme";
			MessageBoxResult dialog = MessageBox.Show(msg,
									  title,
									  MessageBoxButton.YesNo,
									  MessageBoxImage.Question);
			if (dialog == MessageBoxResult.Yes)
			{
				await _SS.ChangeThemeAsync();
				//string desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				//string relativePath = @"ProcessTimeChecker\ProcessTimeCheckerWPF\bin\Debug\net8.0-windows\ProcessTimeCheckerWPF.exe";
				//string filePath = Path.Combine(desktopDirectory, relativePath);
				//Process.Start(filePath);
				//Application.Current.Shutdown();

			}
			else
			{
				await _SS.ChangeThemeAsync();
			}
		}
		private async Task SetCurrentTheme()
		{
			string currentTheme = await _SS.WhatThemeIsIt();
			if (!taskDataGrid.Resources.Contains(typeof(DataGrid)))
			{
				Style dataGridStyle = new Style(typeof(DataGrid));
				dataGridStyle.Setters.Add(new Setter(DataGrid.BackgroundProperty, currentTheme == "Dark" ? Brushes.Black : Brushes.White));
				taskDataGrid.Resources.Add(typeof(DataGrid), dataGridStyle);
			}

			if (!taskDataGrid.Resources.Contains(typeof(DataGridRow)))
			{
				Style dataGridRowStyle = new Style(typeof(DataGridRow));
				dataGridRowStyle.Setters.Add(new Setter(DataGridRow.BackgroundProperty, currentTheme == "Dark" ? Brushes.Black : Brushes.White));
				taskDataGrid.Resources.Add(typeof(DataGridRow), dataGridRowStyle);
			}

			if (!taskDataGrid.Resources.Contains(typeof(DataGridCell)))
			{
				Style dataGridCellStyle = new Style(typeof(DataGridCell));
				dataGridCellStyle.Setters.Add(new Setter(DataGridCell.ForegroundProperty, currentTheme == "Dark" ? Brushes.LightGray : Brushes.Black));
				taskDataGrid.Resources.Add(typeof(DataGridCell), dataGridCellStyle);
			}
		}
	}
}
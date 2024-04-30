using PTC.Domain.Dtos;
using PTC.Domain.GlobalClasses;
using PTC.Services.Services;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
		private static string currentLanguage = null!;
		private static string currentTheme = null!;
		public MainWindow()
		{
			InitializeComponent();
		}
		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			currentLanguage = await _SS.GetLanguage();
			await UpdateTopMost();
			await SetCurrentTheme();
			await SetLanguageSettings();
			int refreshTime = await _SS.GetRefreshTime();
			myTimer.Tick += new EventHandler(TimerEventProcessor);
			myTimer.Interval = TimeSpan.FromSeconds(refreshTime);
			myTimer.Start();
		}
		private async void TimerEventProcessor(object? sender, EventArgs e)
		{
			taskDataGrid.ItemsSource = new List<TasksDto>();
			tasksDtos = await _PS.GetTheProcesses();
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
		private async Task SetLanguageSettings()
		{
			int refreshTime = await _SS.GetRefreshTime();

			AddNewAppTop.Header = currentLanguage == "Turkish" ? "Uygulamalar" : "Apps";
			SettingsTop.Header = currentLanguage == "Turkish" ? "Ayarlar" : "Settings";
			StayOnTop.Header = currentLanguage == "Turkish" ? "Tepede Kalsın" : "Stay on Top";
			ChangeTheme.Header = currentLanguage == "Turkish" ? "Temayı Değiştir" : "Change Theme";
			ChangeRefreshTimer.Header = currentLanguage == "Turkish" ? "Tekrarlama Süresini Ayarla" : "Choose Refresh Timer";
			AddNewApp.Header = currentLanguage == "Turkish" ? "Yeni Uygulama Ekle" : "Add New App";
			refreshRateLabel.Content = currentLanguage == "Turkish" ? $"Tekrarlama Hızı: {refreshTime}" : $"Refresh Rate: {refreshTime}";
			ChangeLanguage.Header = currentLanguage == "Turkish" ? "Dili Değiştir" : "Change Language";
		}
		private async void ChangeTheme_Click(object sender, RoutedEventArgs e)
		{
			myTimer.Stop();

			string msg = currentLanguage == "Turkish" ? "Tema değiştirmek istiyor musunuz? Bu, uygulamayı yeniden başlatacaktır. Eğer yeniden başlatmazsanız, tema değişikliği bir sonraki açılışınızda uygulanacaktır." :
											"Would you like to change the theme? This will restart the app. If you don't choose to restart the app, the theme change will be applied on your next launch.";


			string title = currentLanguage == "Turkish" ? "Tema Değişikliği" : "Change Theme";
			MessageBoxResult dialog = MessageBox.Show(msg,
									  title,
									  MessageBoxButton.YesNo,
									  MessageBoxImage.Question);

			if (dialog == MessageBoxResult.Yes)
			{
				await _SS.ChangeThemeAsync();
				RestartApplication();
			}
			else
			{
				await _SS.ChangeThemeAsync();

				myTimer.Start();
			}
		}
		//private async Task SetDataGridHeadersAsync()
		//{
		//	if (taskDataGrid.Columns.Count > 0)
		//	{
		//		await Application.Current.Dispatcher.InvokeAsync(new Action(() =>
		//		{
		//			taskDataGrid.Columns[0].Header = currentLanguage == "Turkish" ? "Uygulama Adı" : "Task Name";
		//			taskDataGrid.Columns[1].Header = currentLanguage == "Turkish" ? "Çalışma Saati" : "Up Time";
		//			taskDataGrid.Columns[2].Header = currentLanguage == "Turkish" ? "Açılış Saat ve Tarihi" : "Start Date & Time";
		//		}));
		//	}
		//}
		private void RestartApplication()
		{
			Process.Start(GlobalVariables.currentExecutablePath);
			Application.Current.Shutdown();
		}
		private async Task SetCurrentTheme()
		{
			currentTheme = await _SS.WhatThemeIsIt();
			Color color = (Color)ColorConverter.ConvertFromString("#191C20");

			if (!taskDataGrid.Resources.Contains(typeof(DataGrid)))
			{
				Style dataGridStyle = new Style(typeof(DataGrid));
				dataGridStyle.Setters.Add(new Setter(DataGrid.BackgroundProperty, currentTheme == "Dark" ? new SolidColorBrush(color) : Brushes.White));
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
				dataGridCellStyle.Setters.Add(new Setter(DataGridCell.ForegroundProperty, currentTheme == "Dark" ? Brushes.Chartreuse : Brushes.Black));
				taskDataGrid.Resources.Add(typeof(DataGridCell), dataGridCellStyle);
			}
			if (!taskDataGrid.Resources.Contains(typeof(DataGridColumnHeader)))
			{
				Style columnHeaderStyle = new Style(typeof(DataGridColumnHeader));
				columnHeaderStyle.Setters.Add(new Setter(BackgroundProperty, currentTheme == "Dark" ? new SolidColorBrush(color) : Brushes.Gray));
				columnHeaderStyle.Setters.Add(new Setter(ForegroundProperty, currentTheme == "Dark" ? Brushes.AliceBlue : Brushes.White));
				columnHeaderStyle.Setters.Add(new Setter(FontSizeProperty, 14.0));
				columnHeaderStyle.Setters.Add(new Setter(FontWeightProperty, FontWeights.Bold));
				columnHeaderStyle.Setters.Add(new Setter(BorderBrushProperty, Brushes.LightGray));
				columnHeaderStyle.Setters.Add(new Setter(BorderThicknessProperty, new Thickness(0, 0, 0, 2)));
				columnHeaderStyle.Setters.Add(new Setter(PaddingProperty, new Thickness(10)));
				columnHeaderStyle.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Stretch));
				columnHeaderStyle.Setters.Add(new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Center));
				columnHeaderStyle.Setters.Add(new Setter(VerticalContentAlignmentProperty, VerticalAlignment.Center));
				taskDataGrid.ColumnHeaderStyle = columnHeaderStyle;
			}
		}
		private void ChangeRefreshTimer_Click(object sender, RoutedEventArgs e)
		{
			ChooseTimer chooseTimer = new ChooseTimer();
			chooseTimer.ShowDialog();
		}
		private async void ChangeLanguage_Click(object sender, RoutedEventArgs e)
		{
			myTimer.Stop();
			string msg = currentLanguage == "Turkish" ? "Uygulama dilini İngilizce'ye çevirmek istediğinizden emin misiniz?" : "Are you sure you want to change the application language to Turkish?";

			string title = currentLanguage == "Turkish" ? "Dili Değiştir" : "Change Language";
			MessageBoxResult dialog = MessageBox.Show(msg,
									  title,
									  MessageBoxButton.YesNo,
									  MessageBoxImage.Question);

			if (dialog == MessageBoxResult.Yes)
			{
				await _SS.SetLanguage();
				RestartApplication();
			}
			else
			{
				myTimer.Start();
				return;
			}
		}
	}
}
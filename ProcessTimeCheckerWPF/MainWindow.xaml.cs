using PTC.Domain.Dtos;
using PTC.Services.Services;
using System.Windows;
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
			myTimer.Tick += new EventHandler(TimerEventProcessor);
			myTimer.Interval = TimeSpan.FromSeconds(2);
			myTimer.Start();
		}
		private async void TimerEventProcessor(object? sender, EventArgs e)
		{
			dataGrid.ItemsSource = new List<TasksDto>();
			tasksDtos = (List<TasksDto>)await _PS.GetTheProcesses();
			dataGrid.ItemsSource = tasksDtos;
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
	}
}
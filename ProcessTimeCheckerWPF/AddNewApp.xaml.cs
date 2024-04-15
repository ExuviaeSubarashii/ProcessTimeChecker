using PTC.Services.Services;
using System.Windows;

namespace ProcessTimeCheckerWPF
{
	/// <summary>
	/// Interaction logic for AddNewApp.xaml
	/// </summary>
	public partial class AddNewApp : Window
	{
		private readonly ProcessServices _PS = new();
		public AddNewApp()
		{
			InitializeComponent();
		}

		private async Task GetCurrentTasks()
		{
			List<string> tasks = await _PS.GetCurrentlyAddedTasks();
			if (tasks == null)
			{
				return;
			}

			if (tasks != null)
			{
				foreach (var task in tasks)
				{
					listView.Items.Add(task);
				}
			}

		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			await GetCurrentTasks();
		}

		private void listView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{

		}
	}
}

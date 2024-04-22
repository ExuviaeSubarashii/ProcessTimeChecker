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
		private readonly SettingsService _SS = new();
		public static string selectedTask = "";
		private static string currentLanguage = null!;


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
				Application.Current.Dispatcher.Invoke(new Action(() =>
				{
					foreach (var task in tasks)
					{
						listView.Items.Add(task);
					}
				}));
			}

		}
		private void ClearListview()
		{

			listView.Items.Clear();
		}
		private void ClearTextBox()
		{

			NewTaskNameBox.Text = "";

		}
		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			currentLanguage = await _SS.GetLanguage();
			SetLanguage();
			await GetCurrentTasks();
		}

		private void listView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (listView.SelectedItems.Count > 0)
			{
				string item = (string)listView.SelectedItem;
				selectedTask = item;
			}
		}

		private async void ClearFile_Click(object sender, RoutedEventArgs e)
		{
			await FileService.ClearFileContentAsync();
		}

		private async void AddNewAppButton_Click(object sender, RoutedEventArgs e)
		{
			string taskName = NewTaskNameBox.Text;
			if (!string.IsNullOrEmpty(taskName) || !string.IsNullOrWhiteSpace(taskName))
			{
				await AddNewTask(taskName);
			}
		}
		private async Task AddNewTask(string taskName)
		{
			await _PS.SaveTaskInformation(taskName);
			ClearListview();
			ClearTextBox();
			await Task.Run(() => GetCurrentTasks());
		}

		private async void listView_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == System.Windows.Input.Key.Delete && !string.IsNullOrEmpty(selectedTask) && !string.IsNullOrWhiteSpace(selectedTask))
			{
				string msg = currentLanguage == "Turkish" ? $"{selectedTask} uygulamasını izleme listesinden çıkarmak istediğinize emin misiniz? Sonradan tekrar ekleyebilirsiniz."
										   : $"Are you sure you want to remove task {selectedTask}? You will be able to add it again if you change your mind.";

				string title = currentLanguage == "Turkish" ? "Uygulama Kaldırma" : "Remove Application";


				MessageBoxResult dialog = MessageBox.Show(msg,
										  title,
										  MessageBoxButton.YesNo,
										  MessageBoxImage.Question);
				if (dialog == MessageBoxResult.Yes)
				{
					await Task.Run(() => _PS.DeleteTask(selectedTask));
					ClearListview();
					await GetCurrentTasks();
					ClearTextBox();
				}
				else
				{
					ClearListview();
					await GetCurrentTasks();
					ClearTextBox();
				}
			}
		}
		private void SetLanguage()
		{
			ClearFileText.Text = currentLanguage == "Turkish" ? "Dosyayı Temizle" : "Clear File";
			AddNewAppText.Text = currentLanguage == "Turkish" ? "Yeni Uygulama Ekle" : "Add New App";
			InformationLabel.Content = currentLanguage == "Turkish" ? "Silmek İçin Uygulamaya tıkla ve DEL tuşuna bas" : "Click on the app and hit DEL button to Delete";
		}
	}
}

using PTC.Services.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace ProcessTimeCheckerWPF
{
   /// <summary>
   /// Interaction logic for AddNewApp.xaml
   /// </summary>
   public partial class AddNewApp : Window, INotifyPropertyChanged
   {
	 public AddNewApp()
	 {
	    DataContext = this;
	    _currentTasks = new();
	    InitializeComponent();
	 }
	 private readonly ProcessServices _PS = new();

	 private readonly SettingsService _SS = new();

	 private string _selectedTask = "";

	 private string _currentLanguage = null!;

	 public ObservableCollection<string> _currentTasks { get; set; } = new();
	 public ObservableCollection<string> CurrentTasks
	 {
	    get => _currentTasks;
	    set
	    {
		  _currentTasks = value;
		  OnPropertyChanged(nameof(CurrentTasks));
	    }
	 }
	 public event PropertyChangedEventHandler? PropertyChanged;

	 protected void OnPropertyChanged(string propertyName)
	 {
	    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	 }
	 private async Task GetCurrentTasks()
	 {
	    CurrentTasks = new ObservableCollection<string>(await _PS.GetCurrentlyAddedTasks(CancellationToken.None));
	 }
	 private async Task ClearListview()
	 {
	    await GetCurrentTasks();
	    listView.ItemsSource = CurrentTasks;
	 }
	 private void ClearTextBox()
	 {
	    NewTaskNameBox.Text = "";
	 }
	 private async void Window_Loaded(object sender, RoutedEventArgs e)
	 {
	    _currentLanguage = await _SS.GetLanguage();
	    SetLanguage();
	    await GetCurrentTasks();
	 }
	 private void listView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
	 {
	    if (listView.SelectedItems.Count > 0)
	    {
		  string item = (string)listView.SelectedItem;
		  _selectedTask = item;
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
	    await _PS.SaveTaskInformation(taskName, CancellationToken.None);
	    ClearListview();
	    ClearTextBox();
	    await Task.Run(() => GetCurrentTasks());
	 }
	 private async void listView_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
	 {
	    if (e.Key == System.Windows.Input.Key.Delete && !string.IsNullOrEmpty(_selectedTask) && !string.IsNullOrWhiteSpace(_selectedTask))
	    {
		  string msg = _currentLanguage == "Turkish"
			 ? $"{_selectedTask} uygulamasını izleme listesinden çıkarmak istediğinize emin misiniz? Sonradan tekrar ekleyebilirsiniz."
			 : $"Are you sure you want to remove task {_selectedTask}? You will be able to add it again if you change your mind.";

		  string title = _currentLanguage == "Turkish" ? "Uygulama Kaldırma" : "Remove Application";


		  MessageBoxResult dialog = MessageBox.Show(msg,
			 title,
			 MessageBoxButton.YesNo,
			 MessageBoxImage.Question);
		  if (dialog == MessageBoxResult.Yes)
		  {
			await _PS.DeleteTask(_selectedTask, CancellationToken.None);
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
	    ClearFileText.Text = _currentLanguage == "Turkish" ? "Dosyayı Temizle" : "Clear File";
	    AddNewAppText.Text = _currentLanguage == "Turkish" ? "Yeni Uygulama Ekle" : "Add New App";
	    InformationLabel.Content = _currentLanguage == "Turkish" ? "Silmek İçin Uygulamaya tıkla ve DEL tuşuna bas" : "Click on the app and hit DEL button to Delete";
	    howToAddText.Content = _currentLanguage == "Turkish"
		   ? "Yeni bir uygulama eklemek için CTRL+SHIFT+ESC tuşlarına basın veya Görev Yöneticisini açın. Ardından eklemek istediğiniz uygulamanın işlem adını .exe olmadan girin."
		   : "To add a new application press CTRL+SHIFT+ESC keys or open Task Manager. Then write the process name of the application you want to add without the .exe part.";
	 }

   }
}

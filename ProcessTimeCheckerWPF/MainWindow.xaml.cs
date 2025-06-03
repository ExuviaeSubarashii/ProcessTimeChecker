using PTC.Domain.Dtos;
using PTC.Domain.GlobalClasses;
using PTC.Services.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace ProcessTimeCheckerWPF
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window, INotifyPropertyChanged
   {

	 public MainWindow()
	 {
	    DataContext = this;
	    _tasksDtos = new();
	    InitializeComponent();

	 }

	 private readonly DispatcherTimer myTimer = new DispatcherTimer();
	 public ObservableCollection<TasksDto> _tasksDtos { get; set; } = new();
	 public ObservableCollection<TasksDto> TasksDtos
	 {
	    get => _tasksDtos;
	    set
	    {
		  _tasksDtos = value;
		  OnPropertyChanged(nameof(TasksDtos));
	    }
	 }
	 public List<TasksDto> selectedTasks = new List<TasksDto>();
	 private readonly ProcessServices _PS = new();
	 private readonly SettingsService _SS = new();
	 private string currentLanguage = null!;
	 private string currentTheme = null!;
	 private object? _lastSelectedItemKey = null;

	 public event PropertyChangedEventHandler? PropertyChanged;
	 protected void OnPropertyChanged(string propertyName)
	 {
	    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	 }
	 private async void Window_Loaded(object sender, RoutedEventArgs e)
	 {
	    currentLanguage = await _SS.GetLanguage();
	    await UpdateTopMost();
	    await SetCurrentTheme();
	    await SetLanguageSettings();
	    await SetDataGridHeadersAsync();
	    await SetDataGridData();

	    int refreshTime = await _SS.GetRefreshTime();
	    myTimer.Tick += new EventHandler(TimerEventProcessor);
	    myTimer.Interval = TimeSpan.FromSeconds(refreshTime);
	    myTimer.Start();
	 }
	 private async void TimerEventProcessor(object? sender, EventArgs e)
	 {
	    // Store the key of the selected item (e.g., ProcessName)
	    if (taskDataGrid.SelectedItem is TasksDto selected)
	    {
		  _lastSelectedItemKey = selected.ProcessName;
	    }
	    else
	    {
		  _lastSelectedItemKey = null;
	    }

	    await SetDataGridData();

	    // Restore selection after data is updated
	    if (_lastSelectedItemKey is string key)
	    {
		  var itemToSelect = TasksDtos.FirstOrDefault(x => x.ProcessName == key);
		  if (itemToSelect != null)
		  {
			taskDataGrid.SelectedItem = itemToSelect;
			taskDataGrid.ScrollIntoView(itemToSelect);
		  }
	    }
	 }
	 private async Task SetDataGridData()
	 {
	    TasksDtos = new ObservableCollection<TasksDto>(await _PS.GetTheProcesses(CancellationToken.None));
	 }
	 private async Task UpdateTopMost()
	 {
	    bool isTopMost = await Task.Run(() => _SS.IsTopMostAsync());
	    this.Topmost = isTopMost;
	    if (isTopMost is true)
	    {
		  WindowBorder.BorderBrush = Brushes.Blue;
		  WindowBorder.BorderThickness = new Thickness(5);
	    }
	    else
	    {
		  WindowBorder.BorderBrush = Brushes.Transparent;
		  WindowBorder.BorderThickness = new Thickness(0);
	    }

	 }
	 private async void StayOnTop_Click(object sender, RoutedEventArgs e)
	 {
	    await _SS.ChangeTopMostPropertyAsync();
	    await UpdateTopMost();
	 }
	 private async void AddNewApp_Click(object sender, RoutedEventArgs e)
	 {
	    AddNewApp newApp = new AddNewApp();
	    newApp.ShowDialog();
	    await SetDataGridData();
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
	    RemoveTask.Header = currentLanguage == "Turkish" ? "Listeden Kaldır" : "Remove Task";
	    RefreshDataGrid.Header = currentLanguage == "Turkish" ? "Yenile" : "Refresh";
	    KillTask.Header = currentLanguage == "Turkish" ? "İşlemi Sonlandır" : "Kill Task";
	 }
	 private async void ChangeTheme_Click(object sender, RoutedEventArgs e)
	 {

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

	    }
	 }
	 private async Task SetDataGridHeadersAsync()
	 {
	    if (taskDataGrid.Columns.Count > 0)
	    {
		  await Application.Current.Dispatcher.InvokeAsync(new Action(() =>
		  {
			dataGridColumnTaskName.Header = currentLanguage == "Turkish" ? "Uygulama Adı" : "Task Name";
			dataGridColumnTaskHour.Header = currentLanguage == "Turkish" ? "Çalışma Saati" : "Up Time";
			dataGridColumnTaskOpening.Header = currentLanguage == "Turkish" ? "Açılış Saat ve Tarihi" : "Start Date & Time";
		  }));
	    }
	 }
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
	 }
	 private async void RemoveTask_Click(object sender, RoutedEventArgs e)
	 {
	    myTimer.Stop();

	    if (taskDataGrid.SelectedItem is TasksDto selectedItem && taskDataGrid.SelectedItems.Count <= 1)
	    {
		  await _PS.DeleteTask(selectedItem.ProcessName.ToLower(), CancellationToken.None);
		  await SetDataGridData();
	    }
	    else if (taskDataGrid.SelectedItems.Count > 1)
	    {
		  await _PS.BulkDeleteTasks(selectedTasks, CancellationToken.None);
		  await SetDataGridData();
		  selectedTasks.Clear();
	    }
	    myTimer.Start();


	 }
	 private async void RefreshDataGrid_Click(object sender, RoutedEventArgs e)
	 {
	    await SetDataGridData();
	 }
	 private async void KillTask_Click(object sender, RoutedEventArgs e)
	 {
	    if (taskDataGrid.SelectedItems.Count >= 1)
	    {
		  List<TasksDto> selectedItem = taskDataGrid.SelectedItems.Cast<TasksDto>().ToList();
		  foreach (var multipleTasks in selectedItem)
		  {
			var processes = Process.GetProcesses().Where(x => x.ProcessName == multipleTasks.ProcessName);
			foreach (var currTask in processes)
			{
			   currTask.Kill();
			}
		  }
		  await SetDataGridData();
	    }
	 }
	 private void taskDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
	 {
	    if (e.OriginalSource is ScrollViewer)
	    {
		  selectedTasks.Clear();
		  taskDataGrid.SelectedItems.Clear();
		  PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedTasks)));

	    }
	    if (taskDataGrid.SelectedItems.Count >= 1 || Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.LeftCtrl))
	    {
		  List<TasksDto> selectedItem = taskDataGrid.SelectedItems.Cast<TasksDto>().ToList();
		  if (selectedItem.Count > 0)
		  {
			selectedTasks.Clear();

			foreach (var item in selectedItem)
			{
			   selectedTasks.Add(item);
			}
		  }
	    }
	    else
	    {
		  return;
	    }
	 }

	 private void ShowWorkSpaces_Click(object sender, RoutedEventArgs e)
	 {
	    WorkSpaces workSpaces = new WorkSpaces();
	    workSpaces.ShowDialog();
	 }
   }
}
using Microsoft.Win32;
using PTC.Domain.Dtos;
using PTC.Services.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ProcessTimeCheckerWPF
{
   /// <summary>
   /// Interaction logic for WorkSpaces.xaml
   /// </summary>
   public partial class WorkSpaces : Window
   {
	 private readonly WorkspaceService _WS = new WorkspaceService();
	 public ObservableCollection<WorkSpaceDto> _workspaceDtos { get; set; } = new();
	 public OpenFileDialog openFileDialog = new OpenFileDialog
	 {
	    Multiselect = true,
	 };
	 List<string> links = new();
	 public ObservableCollection<WorkSpaceDto> WorkspaceDtos
	 {
	    get => _workspaceDtos;
	    set
	    {
		  _workspaceDtos = value;
		  OnPropertyChanged(nameof(WorkspaceDtos));
	    }
	 }
	 public event PropertyChangedEventHandler? PropertyChanged;
	 protected void OnPropertyChanged(string propertyName)
	 {
	    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	 }
	 public WorkSpaces()
	 {
	    InitializeComponent();

	 }
	 public async Task RunWorkSpace(Guid workspaceId)
	 {
	    try
	    {
		  var workSpace = await _WS.GetWorkSpaceById(workspaceId);
		  foreach (var item in workSpace.WorkSpaceItems)
		  {

			Process newProcess = new Process();
			newProcess.StartInfo.FileName = item;
			newProcess.StartInfo.UseShellExecute = true;
			newProcess.StartInfo.Arguments = "-n";
			//newProcess.StartInfo.Verb = "runas";
			newProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			newProcess.Start();
			await Task.Delay(1000);
		  }
	    }
	    catch (Exception)
	    {

		  throw;
	    }

	 }
	 private async Task SetDataGridWorkSpaceData()
	 {
	    WorkspaceDtos = new ObservableCollection<WorkSpaceDto>(await _WS.GetAllWorkSpaces());
	    workSpaceDataGrid.ItemsSource = WorkspaceDtos;
	 }
	 private async void Window_Loaded(object sender, RoutedEventArgs e)
	 {
	    await _WS.CreateWorkSpaceFileIfDoesntExists();
	    await SetDataGridWorkSpaceData();

	 }

	 private async void workSpaceDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
	 {
	    if (workSpaceDataGrid.SelectedItem is WorkSpaceDto selectedWorkSpace)
	    {
		  Guid guid = selectedWorkSpace.WorkSpaceId ?? Guid.Empty;
		  await RunWorkSpace(guid);
	    }
	 }


	 private async void AddWorkSpaceButton_Click(object sender, RoutedEventArgs e)
	 {
	    List<string> fullList = new();
	    fullList.AddRange(openFileDialog.FileNames.ToList());
	    fullList.AddRange(links);
	    WorkSpaceDto newWorkSpace = new WorkSpaceDto
	    {
		  WorkSpaceId = Guid.NewGuid(),
		  WorkSpaceName = WorkSpaceNameInput.Text,
		  WorkSpaceDescription = WorkSpaceDescriptionInput.Text,
		  WorkSpaceItems = fullList
	    };
	    await _WS.CreateNewWorkSpace(newWorkSpace);
	    await SetDataGridWorkSpaceData();
	    WorkSpaceNameTextBox.Text = string.Empty;
	    WorkSpaceDescriptionTextBox.Text = string.Empty;
	    fullList.Clear();
	    links.Clear();
	    openFileDialog.FileName = string.Empty;
	 }

	 private void EditWorkSpace_Click(object sender, RoutedEventArgs e)
	 {
	    if (workSpaceDataGrid.SelectedItem is WorkSpaceDto)
	    {
		  var currentWorkSpace = workSpaceDataGrid.SelectedItem as WorkSpaceDto;
		  EditWorkspaceWindow editWorkSpace = new EditWorkspaceWindow(currentWorkSpace);
		  editWorkSpace.Closed += async (s, args) => await SetDataGridWorkSpaceData();
		  editWorkSpace.Show();
	    }
	 }

	 private async void DeleteWorkSpace_Click(object sender, RoutedEventArgs e)
	 {
	    if (workSpaceDataGrid.SelectedItem is WorkSpaceDto selectedWorkSpace)
	    {
		  await _WS.RemoveWorkSpace(selectedWorkSpace);
		  await SetDataGridWorkSpaceData();
	    }
	 }

	 private void ComboBoxOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
	 {
	    var comboBox = sender as ComboBox;
	    var selectedItem = comboBox.SelectedItem as ComboBoxItem;

	    if (selectedItem == ChooseFilesButton)
	    {
		  openFileDialog.ShowDialog();

	    }
	    else if (selectedItem == AddLinkButton)
	    {
		  Window textBoxWindow = new Window
		  {
			Title = "Add Link",
			Width = 300,
			Height = 200,
			WindowStartupLocation = WindowStartupLocation.CenterScreen
		  };

		  // Create TextBox
		  TextBox linkTextBox = new TextBox
		  {
			Margin = new Thickness(10),
			Height = 30
		  };

		  // Create Button
		  Button button = new Button
		  {
			Content = "Add Link",
			Margin = new Thickness(10),
			Height = 30
		  };

		  // StackPanel for layout
		  StackPanel stackPanel = new StackPanel();
		  stackPanel.Children.Add(linkTextBox);
		  stackPanel.Children.Add(button);

		  // Set content of the window
		  textBoxWindow.Content = stackPanel;

		  // Button click event
		  button.Click += (s, args) =>
		  {
			string link = linkTextBox.Text;
			links.Add(link);
			textBoxWindow.Close(); // Optional: close window after clicking
		  };

		  // Show the window
		  textBoxWindow.ShowDialog(); // Use Show() if you don't want modal behavior

	    }

	    // Optionally reset selection so user can click again later
	    comboBox.SelectedIndex = -1;
	 }
   }
}

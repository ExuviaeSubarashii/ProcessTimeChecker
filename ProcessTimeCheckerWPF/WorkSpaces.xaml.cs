using Microsoft.Win32;
using PTC.Domain.Dtos;
using PTC.Services.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

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
			newProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			newProcess.Start();
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

	 private void ChooseFilesButton_Click(object sender, RoutedEventArgs e)
	 {
	    openFileDialog.ShowDialog();
	 }

	 private async void AddWorkSpaceButton_Click(object sender, RoutedEventArgs e)
	 {
	    WorkSpaceDto newWorkSpace = new WorkSpaceDto
	    {
		  WorkSpaceId = Guid.NewGuid(),
		  WorkSpaceName = WorkSpaceNameInput.Text,
		  WorkSpaceDescription = WorkSpaceDescriptionInput.Text,
		  WorkSpaceItems = openFileDialog.FileNames.ToList()
	    };
	    await _WS.CreateNewWorkSpace(newWorkSpace);
	    await SetDataGridWorkSpaceData();

	 }

	 private void EditWorkSpace_Click(object sender, RoutedEventArgs e)
	 {
	    var currentWorkSpace = workSpaceDataGrid.SelectedItem as WorkSpaceDto;
	    EditWorkspaceWindow editWorkSpace = new EditWorkspaceWindow(currentWorkSpace);
	    editWorkSpace.Closed += async (s, args) => await SetDataGridWorkSpaceData();
	    editWorkSpace.Show();
	 }
   }
}

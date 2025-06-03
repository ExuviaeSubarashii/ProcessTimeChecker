using Microsoft.Win32;
using PTC.Domain.Dtos;
using PTC.Services.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace ProcessTimeCheckerWPF
{
   /// <summary>
   /// Interaction logic for EditWorkspaceWindow.xaml
   /// </summary>
   public partial class EditWorkspaceWindow : Window
   {
	 private readonly WorkspaceService _WS = new();
	 public static WorkSpaceDto EditWorkSpaceDto { get; set; } = new();
	 public ObservableCollection<string> FilePaths { get; set; } = new ObservableCollection<string>();
	 public ObservableCollection<string> _workspaceDtos { get; set; } = new();
	 public ObservableCollection<string> WorkspaceDtos
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
	 public OpenFileDialog openFileDialog = new OpenFileDialog
	 {
	    Multiselect = true,
	 };
	 public EditWorkspaceWindow(WorkSpaceDto _editWorkSpaceDto)
	 {
	    InitializeComponent();
	    EditWorkSpaceDto = _editWorkSpaceDto;
	    openFileDialog.FileOk += OpenFileDialog_FileOk;
	 }

	 private void OpenFileDialog_FileOk(object? sender, System.ComponentModel.CancelEventArgs e)
	 {
	    foreach (var item in openFileDialog.FileNames)
	    {
		  FilePaths.Add(item);
	    }
	 }

	 private void Window_Loaded(object sender, RoutedEventArgs e)
	 {
	    WorkSpaceNamePlaceHolderTextBox.Text = EditWorkSpaceDto.WorkSpaceName ?? string.Empty;
	    WorkSpaceDescriptionPlaceHolderTextBox.Text = EditWorkSpaceDto.WorkSpaceDescription ?? string.Empty;
	    foreach (var item in EditWorkSpaceDto.WorkSpaceItems)
	    {
		  FilePaths.Add(item);
	    }
	    WorkSpaceFilesListView.ItemsSource = FilePaths;
	 }

	 private async void SaveChangesButton_Click(object sender, RoutedEventArgs e)
	 {
	    WorkSpaceDto editedWorkSpace = new()
	    {
		  WorkSpaceId = EditWorkSpaceDto.WorkSpaceId ?? Guid.NewGuid(),
		  WorkSpaceDescription = WorkSpaceDescriptionPlaceHolderTextBox.Text,
		  WorkSpaceName = WorkSpaceNamePlaceHolderTextBox.Text,
		  WorkSpaceItems = WorkSpaceFilesListView.Items.Cast<string?>().ToList(),
	    };
	    await _WS.UpdateWorkSpace(editedWorkSpace);
	 }

	 private void AddNewFileButton_Click(object sender, RoutedEventArgs e)
	 {
	    openFileDialog.ShowDialog();
	 }
   }
}

using Microsoft.Win32;
using PTC.Domain.Dtos;
using PTC.Services.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProcessTimeCheckerWPF
{
   /// <summary>
   /// Interaction logic for EditWorkspaceWindow.xaml
   /// </summary>
   public partial class EditWorkspaceWindow : Window
   {
	 private readonly WorkspaceService _WS = new();
	 Point _dragStartPoint;
	 object _draggedItem;
	 public static WorkSpaceDto EditWorkSpaceDto { get; set; } = new();
	 public ObservableCollection<string?> FilePaths { get; set; } = new ObservableCollection<string?>();
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
		  WorkSpaceItems = FilePaths.ToList()
	    };
	    await _WS.UpdateWorkSpace(editedWorkSpace);
	    this.Close();
	 }

	 private void AddNewFileButton_Click(object sender, RoutedEventArgs e)
	 {
	    openFileDialog.ShowDialog();
	 }

	 private void ShowInExplorer_Click(object sender, RoutedEventArgs e)
	 {
	    if (WorkSpaceFilesListView.SelectedItem is string selectedFile)
	    {

	    }
	 }


	 private void RemoveFromList_Click(object sender, RoutedEventArgs e)
	 {
	    if (WorkSpaceFilesListView.SelectedItem is string selectedFile)
	    {
		  FilePaths.Remove(selectedFile);
		  WorkSpaceFilesListView.ItemsSource = FilePaths;
	    }
	 }

	 private void WorkSpaceFilesListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	 {
	    _dragStartPoint = e.GetPosition(null);
	    var listView = sender as ListView;
	    _draggedItem = GetObjectDataFromPoint(listView, e.GetPosition(listView));
	 }

	 private void WorkSpaceFilesListView_PreviewMouseMove(object sender, MouseEventArgs e)
	 {
	    Point mousePos = e.GetPosition(null);
	    Vector diff = _dragStartPoint - mousePos;

	    if (e.LeftButton == MouseButtonState.Pressed &&
		   (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
		    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
	    {
		  if (_draggedItem != null)
		  {
			DragDrop.DoDragDrop(WorkSpaceFilesListView, _draggedItem, DragDropEffects.Move);
		  }
	    }
	 }

	 private void WorkSpaceFilesListView_Drop(object sender, DragEventArgs e)
	 {
	    if (e.Data.GetDataPresent(typeof(string)))
	    {
		  var droppedData = e.Data.GetData(typeof(string)) as string;
		  var listView = sender as ListView;
		  var target = GetObjectDataFromPoint(listView, e.GetPosition(listView)) as string;

		  if (droppedData == null || target == null || droppedData == target)
			return;

		  var items = WorkSpaceFilesListView.ItemsSource as ObservableCollection<string>;
		  int removedIdx = items.IndexOf(droppedData);
		  int targetIdx = items.IndexOf(target);

		  if (removedIdx < targetIdx)
		  {
			items.Insert(targetIdx + 1, droppedData);
			items.RemoveAt(removedIdx);
		  }
		  else
		  {
			int removeIndex = removedIdx + 1;
			if (items.Count + 1 > removeIndex)
			{
			   items.Insert(targetIdx, droppedData);
			   items.RemoveAt(removeIndex);
			}
		  }
	    }
	 }

	 private object GetObjectDataFromPoint(ListView source, Point point)
	 {
	    var element = source.InputHitTest(point) as DependencyObject;
	    while (element != null && !(element is ListViewItem))
		  element = VisualTreeHelper.GetParent(element);

	    return element != null ? source.ItemContainerGenerator.ItemFromContainer(element) : null;
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
			FilePaths.Add(link);
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

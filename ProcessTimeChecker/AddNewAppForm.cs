using PTC.Domain.Dtos;
using PTC.Services.Services;

namespace ProcessTimeChecker
{
	public partial class AddNewAppForm : Form
	{
		private readonly ProcessServices _PS;

		public AddNewAppForm(ProcessServices PS)
		{
			InitializeComponent();
			_PS = PS;
		}
		private async void AddNewAppForm_Load(object sender, EventArgs e)
		{
			List<CurrentlyAddedTasksDto> tasks = (List<CurrentlyAddedTasksDto>)await GetCurrentTasks();
			if (tasks != null)
			{
				foreach (var task in tasks)
				{
					listView1.Items.Add(task.TaskName);
				}
			}
		}
		private async Task<IEnumerable<CurrentlyAddedTasksDto>> GetCurrentTasks()
		{
			List<CurrentlyAddedTasksDto> tasks = await _PS.GetCurrentlyAddedTasks();
			if (tasks == null)
			{
				return new List<CurrentlyAddedTasksDto>();
			}
			else
			{
				return tasks.ToList();
			}

		}
		private async Task AddNewTask(string taskName)
		{
			await _PS.SaveTaskInformation(taskName);
		}
		private async void button1_Click(object sender, EventArgs e)
		{
			string taskName = textBox1.Text;
			if (!string.IsNullOrEmpty(taskName) || !string.IsNullOrWhiteSpace(taskName))
			{
				await Task.Run(() => AddNewTask(taskName));
			}
		}
	}
}

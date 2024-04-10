using PTC.Services.Services;

namespace ProcessTimeChecker
{
	public partial class AddNewAppForm : Form
	{
		private readonly ProcessServices _PS;
		public static string selectedTask = "";
		public AddNewAppForm(ProcessServices PS)
		{
			InitializeComponent();
			_PS = PS;
		}
		private async void AddNewAppForm_Load(object sender, EventArgs e)
		{
			await GetCurrentTasks();
		}
		private async Task GetCurrentTasks()
		{
			List<string> tasks = await _PS.GetCurrentlyAddedTasks();
			if (tasks == null)
			{
				return;
			}
			else
			{
				if (tasks != null)
				{
					foreach (var task in tasks)
					{
						listView1.Items.Add(task);
					}
				}
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

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listView1.SelectedItems.Count > 0)
			{
				selectedTask = listView1.SelectedItems[0].Text;
			}
		}

		private async void listView1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == (char)Keys.Delete && !string.IsNullOrEmpty(selectedTask) && !string.IsNullOrWhiteSpace(selectedTask))
			{
				string msg = $"{selectedTask} Uygulamasını İzleme Listesinden Çıkarmak İstediğinize Emin Misiniz? Sonradan Tekrar Ekleyebilirsiniz.";
				string title = "Uygulama Kaldırma";
				MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				DialogResult dialog = MessageBox.Show(msg, title, buttons);
				if (dialog == DialogResult.Yes)
				{
					await Task.Run(() => _PS.DeleteTask(selectedTask));
					this.Close();
				}
				else
				{
					this.Close();
				}
			}
		}
	}
}

using PTC.Domain.Dtos;
using PTC.Services.Services;

namespace ProcessTimeChecker
{
	public partial class Form1 : Form
	{

		static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
		public List<TasksDto> tasksDtos = new();
		private readonly ProcessServices _PS;
		private readonly SettingsService _SS;

		public Form1(ProcessServices PS, SettingsService sS)
		{
			_PS = PS;
			_SS = sS;
			InitializeComponent();
		}

		private async void Form1_Load(object sender, EventArgs e)
		{
			await UpdateTopMost();
			myTimer.Tick += new EventHandler(TimerEventProcessor);
			myTimer.Interval = 2000;
			myTimer.Start();
		}

		private async void TimerEventProcessor(object? sender, EventArgs e)
		{
			dataGridView1.DataSource = new List<TasksDto>();
			tasksDtos = (List<TasksDto>)await _PS.GetTheProcesses();
			dataGridView1.DataSource = tasksDtos;
			dataGridView1.Refresh();
		}
		private void yeniUygulamaEkleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddNewAppForm addNewAppForm = new AddNewAppForm(_PS);
			addNewAppForm.ShowDialog();
		}


		private async void checkToolStripMenuItem_Click(object sender, EventArgs e)
		{
			await _SS.ChangeTopMostPropertyAsync();
		}

		private async void stayOnTopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			await _SS.ChangeTopMostPropertyAsync();
			await UpdateTopMost();
		}
		private async Task UpdateTopMost()
		{
			bool isTopMost = await Task.Run(() => _SS.IsTopMostAsync());
			this.TopMost = isTopMost;
		}
	}
}

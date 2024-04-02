using PTC.Domain.Dtos;
using PTC.Services.Services;

namespace ProcessTimeChecker
{
    public partial class Form1 : Form
    {

        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        public List<TasksDto> tasksDtos = new();
        private readonly ProcessServices _PS;
        public Form1(ProcessServices PS)
        {
            _PS = PS;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myTimer.Tick += new EventHandler(TimerEventProcessor);
            myTimer.Interval = 2000;
            myTimer.Start();

        }

        private void TimerEventProcessor(object? sender, EventArgs e)
        {
            dataGridView1.DataSource = new List<TasksDto>();
            string[] arr = ["Code", "devenv", "Spotify", "notepad++"];
            tasksDtos = _PS.GetTheProcesses(arr);
            dataGridView1.DataSource = tasksDtos;

        }

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            List<TasksDto> tasks = new();
            foreach (var item in tasksDtos)
            {
                TasksDto tasks2 = new()
                {
                    TaskClosing = item.TaskClosing,
                    TaskDate = DateTime.Today,
                    TaskHour = Convert.ToInt32(item.TaskHour),
                    TaskName = item.TaskName,
                    TaskOpening = item.TaskOpening
                };
                tasks.Add(tasks2);
            }
            await _PS.SaveTaskInformation(tasks);
        }
    }
}

using PTC.Domain.Dtos;
using PTC.Domain.Models;
using PTC.Services.Services;

namespace ProcessTimeChecker
{
    public partial class Form1 : Form
    {

        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        public List<TasksDto> tasksDtos;
        public Form1()
        {
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
            ProcessServices PS = new();
            dataGridView1.DataSource = new List<TasksDto>();
            string[] arr = ["Code", "devenv", "Spotify", "notepad++"];
            tasksDtos = PS.GetTheProcesses(arr);
            dataGridView1.DataSource = tasksDtos;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProcessServices PS = new();

            List<Tasks> tasks = new();
            foreach (var item in tasksDtos)
            {
                Tasks tasks2 = new()
                {
                    TaskClosing = item.EndTime,
                    TaskDate = DateTime.Today,
                    TaskHour = Convert.ToInt32(item.FullTime),
                    TaskName = item.ProcessName,
                    TaskOpening = item.StartTime
                };
                tasks.Add(tasks2);
            }
            PS.SaveTaskInformation(tasks);
        }
    }
}

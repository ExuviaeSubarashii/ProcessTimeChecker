using PTC.Domain.Dtos;
using PTC.Services.Services;
using System.Timers;

namespace ProcessConsole
{
    internal class Program
    {
        private static System.Timers.Timer fetchTimer;
        ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
        static void Main(string[] args)
        {
            fetchTimer = new System.Timers.Timer(5000);
            fetchTimer.Elapsed += OnTimedEvent;
            fetchTimer.AutoReset = true;
            fetchTimer.Enabled = true;
            Console.ReadKey();
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.Clear();
            WriteProcess();
        }
        private static void WriteProcess()
        {
            ProcessServices processServices = new ProcessServices();
            //"Code", 
            string[] arr = ["devenv", "Spotify", "notepad++"];
            List<TasksDto> tasksDtos = processServices.GetTheProcesses(arr);
            foreach (TasksDto task in tasksDtos)
            {
                Console.WriteLine($"\x1b[31mProcess Name: {task.ProcessName}\x1b[0m");
                Console.WriteLine($"Machine Name: {task.MachineName}");
                Console.WriteLine($"Start Time: {task.StartTime}");
                Console.WriteLine($"End Time: {task.EndTime}");
                Console.WriteLine(task.FullTime);
                Console.WriteLine();
            }
        }
    }
}
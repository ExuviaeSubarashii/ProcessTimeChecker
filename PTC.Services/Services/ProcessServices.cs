using PTC.Domain.Dtos;
using PTC.Domain.Interfaces;
using PTC.Domain.Models;
using System.Diagnostics;

namespace PTC.Services.Services
{
    public class ProcessServices : ILookForProcessInterface
    {
        public List<TasksDto> GetTheProcesses(string[] processName)
        {
            List<TasksDto> tasks = new();

            foreach (var item in processName)
            {
                Process[] localbyname = Process.GetProcessesByName(item);
                if (localbyname != null)
                {
                    for (int j = 0; j < localbyname.Length; j++)
                    {
                        TasksDto dto = new()
                        {
                            TaskName = localbyname[j].ProcessName,
                            TaskOpening = localbyname[j].StartTime,
                            TaskClosing = localbyname[j].HasExited ? localbyname[j].ExitTime : DateTime.Now,
                            TaskHour = (int)(localbyname[j].StartTime - DateTime.Now).TotalHours,
                            TaskDate = DateTime.Now,
                        };
                        tasks.Add(dto);
                    }
                }
            }
            return tasks.ToList();
        }
        private static string FormatTimeSpan(TimeSpan timeSpan)
        {
            // Format the time span as desired
            return $"{Math.Abs(timeSpan.Hours):00}:{Math.Abs(timeSpan.Minutes):00}:{Math.Abs(timeSpan.Seconds):00}.{Math.Abs(timeSpan.Milliseconds):000}";
        }

        public async Task<string> SaveTaskInformation(List<TasksDto> task)
        {
            try
            {
                foreach (var item in task)
                {
                    using (ProcessTimersContext context = new())
                    {
                        TasksSaving tasks = new()
                        {
                            TaskOpening = item.TaskOpening.ToString(),
                            TaskClosing = item.TaskClosing.ToString(),
                            TaskDate = item.TaskDate.ToString(),
                            TaskHour = item.TaskHour.ToString(),
                            TaskName = item.TaskName.ToString(),
                        };
                        context.TasksSaving.Add(tasks);
                        await context.SaveChangesAsync();
                    }
                }
                return "Succesful";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

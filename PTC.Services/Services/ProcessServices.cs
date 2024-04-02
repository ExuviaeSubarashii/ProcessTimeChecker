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
                            ProcessName = localbyname[j].ProcessName,
                            MachineName = localbyname[j].MachineName,
                            StartTime = localbyname[j].StartTime,
                            EndTime = localbyname[j].HasExited ? localbyname[j].ExitTime : DateTime.Now,
                            FullTime = FormatTimeSpan(localbyname[j].StartTime - DateTime.Now),
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

        public Task<string> SaveTaskInformation(List<Tasks> task)
        {
            throw new NotImplementedException();
        }
    }
}

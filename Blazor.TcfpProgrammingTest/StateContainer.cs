using Blazor.TcfpProgrammingTest.Models;

namespace Blazor.TcfpProgrammingTest
{
    public class StateContainer
    {
        static List<Department> departments = new List<Department>()
        {
            new Department { Id = 1, Name = "Austin Fire Department" },
            new Department { Id = 2, Name = "Round Rock Fire Department"},
            new Department { Id = 3, Name = "Georgetown Fire Department"},
            new Department { Id = 4, Name = "Pflugerville Fire Department"}
        };

        static List<IncidentReport> reports = new List<IncidentReport>()
        {
            new IncidentReport
            {
                Id = 1,
                DepartmentId = departments[0].Id,
                Department = departments[0],
                CreatedAt = new DateTime(2022, 1, 15, 20, 12, 15),
                Status = IncidentReportStatus.Closed,
                DateOfIncident = new DateTime(2022, 1, 13, 10, 0, 0),
                Name = "Injured left index finger during training",
                Description = "Injury to left index finger at knuckle. Occurred while participating in forcible entry training exercises at fire academy.",
                Location = "123 Main St Austin, Texas",
                Type = IncidentReportType.Training,
            },
            new IncidentReport
            {
                Id = 2,
                DepartmentId = departments[1].Id,
                Department = departments[1],
                CreatedAt = new DateTime(2022, 4, 14, 17, 44, 36),
                Status = IncidentReportStatus.Pending,
                DateOfIncident = new DateTime(2022, 4, 14, 15, 30, 0),
                Name = "Vehicle Accident",
                Description = "Crashed car during training",
                Location = "Round Rock, Texas",
                Type = IncidentReportType.Accident
            },
            new IncidentReport
            {
                Id = 3,
                DepartmentId = departments[2].Id,
                Department = departments[2],
                CreatedAt = new DateTime(2023, 2, 14, 17, 44, 36),
                Status = IncidentReportStatus.Pending,
                DateOfIncident = new DateTime(2023, 2, 14, 15, 30, 0),
                Name = "Slipped and fell off a roof",
                Description = "Slipped and fell off a roof",
                Location = "Interstate 35, Georgetown, Texas",
                Type = IncidentReportType.Other
            }
        };

        public List<IncidentReport> Reports
        {
            get => reports; 
            set
            {
                reports = value;
                NotifyStateChanged();
            }
        }

        public List<Department> Departments { get => departments; }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}

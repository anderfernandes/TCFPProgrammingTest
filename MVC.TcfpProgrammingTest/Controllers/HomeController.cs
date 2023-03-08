using Microsoft.AspNetCore.Mvc;
using MVC.TcfpProgrammingTest.Models;
using System.Diagnostics;

namespace MVC.TcfpProgrammingTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Count the number of times the user delete the data.
        private static int countDeleteClicked = 0;

        private static List<Department> departments = new List<Department>()
        {
            new Department { Id = 1, Name = "Austin Fire Department" },
            new Department { Id = 2, Name = "Round Rock Fire Department"},
            new Department { Id = 3, Name = "Georgetown Fire Department"},
            new Department { Id = 4, Name = "Pflugerville Fire Department"}
        };

        private static List<IncidentReport> reports = new List<IncidentReport>()
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // TODO: Return a view with the list of reports.
        public IActionResult Index()
        {
            // Create a new list and store the data in an instance.
            var reportsView = reports.Select(r => new IncidentReport
            {
                Id = r.Id,
                DepartmentId = r.DepartmentId,
                Department = r.Department ?? null,
                CreatedAt = r.CreatedAt,
                Status = r.Status,
                DateOfIncident = r.DateOfIncident,
                Name = r.Name ?? "",
                Description = r.Description ?? "",
                Location = r.Location ?? "",
                Type = r.Type
            }).ToList();

            // Pass the list to the view to output the data on the homepage.
            return View(reportsView);
        }

        // TODO: Find a report by id and return it. Return 404 if id doesn't exists.
        [Route("/{id}")]
        public IActionResult Show(int id)
        {
            // Find the first result from the list, return null if report is not found.
            var report = reports.FirstOrDefault(r => r.Id == id);

            // If report is not found return 404.
            if (reports is null) return NotFound();

            // Create a new instance of the existing report
            var reportExist = new IncidentReport
            {
                Id = id,
                DepartmentId = report.DepartmentId,
                Department = report.Department ?? null,
                Type = report.Type,
                Name = report.Name ?? "",
                Description = report.Description ?? "",
                Location = report.Location ?? "",
                DateOfIncident = report.DateOfIncident,
                CreatedAt = DateTime.Now,
                Status = report.Status
            };

            return View(reportExist);
        }

        // TODO: Returns all departments.
        [Route("/{action}")]
        public IActionResult Create()
        {
            // Convert departments to list.
            var allDepartments = departments.ToList();

            return View(allDepartments);
        }

        // Populate ViewData with report that matches the requested id and departments, return view
        [Route("/{id}/{action}")]
        public IActionResult Edit(int id)
        {
            // Find the existing report by its ID.
            var report = reports.FirstOrDefault(r => r.Id == id);

            // If the report ID is not found, return 404.
            if (report is null) return NotFound();

            ViewData["Report"] = report;
            ViewData["Departments"] = departments;

            return View();
        }

        // TODO: Add report to list and redirect to the report's show route.
        [HttpPost]
        public IActionResult Store(int DepartmentId, IncidentReportType Type, string Name, string Description, string Location, string DateOfIncident)
        {
            // If the user doesn't fill in the Date of Incident, return the current date and time.
            if (string.IsNullOrEmpty(DateOfIncident)) DateOfIncident = DateTime.Now.ToString("s");

            // Find the existing department by its ID. 
            var department = departments.FirstOrDefault(r => r.Id == DepartmentId);

            // Parse the reportId value from the submitted form data as an interger.
            // If the parse is successful, the value > 0 is returned.
            // If fails, reportId = 0.
            if (int.TryParse(Request.Form["reportId"], out var reportId))
            {
                // Parse the reportStatus value from the submitted form data as an interger.
                int.TryParse(Request.Form["reportStatus"], out var reportStatusInt);
                // Convert int to IncidentReportStatus.
                var reportStatus = (IncidentReportStatus)Enum.Parse(typeof(IncidentReportStatus), reportStatusInt.ToString());

                // Update an existing report
                var reportToUpdate = reports.FirstOrDefault(r => r.Id == reportId);
                if (reportToUpdate != null)
                {
                    reportToUpdate.DepartmentId = DepartmentId;
                    reportToUpdate.Department = department;
                    reportToUpdate.Type = Type;
                    reportToUpdate.Name = Name;
                    reportToUpdate.Description = Description;
                    reportToUpdate.Location = Location;
                    reportToUpdate.DateOfIncident = DateTime.Parse(DateOfIncident);
                    reportToUpdate.CreatedAt = DateTime.Now;
                    reportToUpdate.Status = reportStatus;
                };
                return RedirectToAction("Show", new { id = reportToUpdate?.Id });
            }
            // If reportId == 0 (doesn't exist), create a new report
            else
            {
                var newReport = new IncidentReport
                {
                    Id = reports.Count + 1 + countDeleteClicked,
                    DepartmentId = DepartmentId,
                    Department = department,
                    Type = Type,
                    Name = Name,
                    Description = Description,
                    Location = Location,
                    DateOfIncident = DateTime.Parse(DateOfIncident),
                    CreatedAt = DateTime.Now,
                    Status = IncidentReportStatus.Open
                };

                // Add new report to the list.
                reports.Add(newReport);
                return RedirectToAction("Show", new { id = newReport.Id });
            }
        }

        // TODO: Delete report and redirect to home page. Return 404 if the report doesn't exist.
        [HttpPost]
        public IActionResult Delete(int id)
        {
            countDeleteClicked++;

            // Find the existing report by its ID. 
            var report = reports.FirstOrDefault(r => r.Id == id);

            // If the report doesn't exist, return a 404 status code.
            if (report == null)
            {
                return NotFound();
            }
            // Otherwise, remove the report from the reports list.
            else
            {
                reports.Remove(report);

                return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
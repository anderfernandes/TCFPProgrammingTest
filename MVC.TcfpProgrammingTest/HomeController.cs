using Microsoft.AspNetCore.Mvc;
using MVC.TcfpProgrammingTest.Models;
using System.Diagnostics;

namespace MVC.TcfpProgrammingTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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
                Type = IncidentReportType.Training
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
            //Grabs the list from IncidentReport and sets reports to list
            List<IncidentReport> reportList = reports.ToList();
            return View(reportList);
        }

        // TODO: Find a report by id and return it. Return 404 if id doesn't exists.
        [Route("/{id}")]
        public IActionResult Show(int id)
        {
            //Passes in id being Id in the Incident Report
            IncidentReport report = reports.FirstOrDefault(r => r.Id == id);
            if(report != null){
                return View(report);
            }

            return NotFound();

        }

        // TODO: Returns all departments.
        [Route("/{action}")]
        public IActionResult Create()
        {
            //Just calls the departments list
            return View(departments);
        }

        // Populate ViewData with report that matches the requested id and departments, return view
        [Route("/{id}/{action}")]
        public IActionResult Edit(int id)
        {
            var report = reports.FirstOrDefault(r => r.Id == id);

            //Only runs when the report is empty/cannot be found
            if (report is null) return NotFound();


            //Populates Viewdata
            ViewData["Report"] = report;
            ViewData["Departments"] = departments;

            return View();
        }

        // TODO: Add report to list and redirect to the report's show route.
        [HttpPost]
        public IActionResult Store(int DepartmentId, IncidentReportType Type, string Name, string Description, string Location, string DateOfIncident)
        {
            // Get the maximum id of the existing IncidentReport objects
            int maxId = reports.Count > 0 ? reports.Max(r => r.Id) : 0;
        
            // Create a new IncidentReport object with the specified parameters
            try
            {
                IncidentReport newReport = new IncidentReport
                {
                    Id = maxId + 1,
                    DepartmentId = DepartmentId,
                    Type = Type,
                    Name = Name,
                    Description = Description,
                    Location = Location,
                    DateOfIncident = DateTime.Parse(DateOfIncident),
                    CreatedAt = DateTime.Now,
                    Status = IncidentReportStatus.Pending
                };

                // Add the new IncidentReport object to the reports list
                reports.Add(newReport);
                return RedirectToAction("Show", new { id = newReport.Id });
            }
            catch (Exception Error) {
                return Redirect("Index");
            }
        }

        private IActionResult Redirect(string v, object value)
        {
            throw new NotImplementedException();
        }

        // TODO: Delete report and redirect to home page. Return 404 if the report doesn't exist.
        [HttpPost]
        public IActionResult Delete(int id) 
        {
            var report = reports.FirstOrDefault(r => r.Id == id);
            if (report == null) 
            {
                return NotFound();
            }
            
            //this removes the current report in this function
            reports.Remove(report);
            return Redirect("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

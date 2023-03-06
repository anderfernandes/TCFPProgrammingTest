using WebApi.TcfpProgrammingTest.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var departments = new List<Department>()
{
    new Department { Id = 1, Name = "Austin Fire Department" },
    new Department { Id = 2, Name = "Round Rock Fire Department"},
    new Department { Id = 3, Name = "Georgetown Fire Department"},
    new Department { Id = 4, Name = "Pflugerville Fire Department"}
};

var reports = new List<IncidentReport>() {
    new IncidentReport
    {
        Id = 1,
        DepartmentId = 1,
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
        DepartmentId = 2,
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
        DepartmentId = 1,
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

// TODO: Returns all departments.
app.MapGet("/departments", () =>
{
    // TODO: Returns all departments.
    // ...
}).WithName("DepartmentsIndex");

// TODO: Returns a single department by id.
app.MapGet("/departments/{id}", (int id) =>
{
    // TODO: Find and return the department that matches the id. Return 404 if not found.
    // ...
});

// TODO: Returns all reports in descending order, newest first.
app.MapGet("/reports", () =>
{
    // TODO: Return the list of reports ordered by id.
    // ...
}).WithName("ReportsIndex");

// TODO: Retuns a report from the list by id.
app.MapGet("/reports/{id}", (int id) =>
{
    // TODO: Find and return the report with HTTP 200. Return 404 if not found.
    // ...
}).WithName("ReportsShow");

// TODO: Saves a new report
app.MapPost("/reports/create", (IncidentReport report) =>
{
    // TODO: Add new report to the list, return HTTP 200 status code.
    // ...
}).WithName("ReportsCreate");

// TODO: Updates a report
app.MapPut("/reports/{id}/update", (IncidentReport report) =>
{
    // TODO: Find the report, update it and return HTTP 200 status code.
    // ...

}).WithName("ReportsUpdate");

// TODO: Deletes a report
app.MapDelete("/reports/{id}/delete", (int id) =>
{
    // TODO: Find the report by id, remove it from the list of reports and return HTTP 200 status code.
    // ...
}).WithName("ReportsDelete");

app.Run();
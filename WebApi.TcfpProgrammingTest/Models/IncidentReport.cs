namespace WebApi.TcfpProgrammingTest.Models
{
    /// <summary>
    /// Defines a TCFP incident report.
    /// </summary>
    public class IncidentReport
    {
        public IncidentReport() { }
        public int Id { get; set; }

        public int DepartmentId { get; set; }

        public Department? Department { get; set; }

        public DateTime CreatedAt { get; set; }

        public IncidentReportStatus Status { get; set; }

        public DateTime DateOfIncident { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }

        public IncidentReportType Type { get; set; }

        public string? Description { get; set; }

    }

    /// <summary>
    /// The possible statuses of an incident report.
    /// </summary>
    public enum IncidentReportStatus
    {
        Open,
        Pending,
        Closed
    }

    /// <summary>
    /// The possible types of incident reports.
    /// </summary>
    public enum IncidentReportType
    {
        Training,
        Accident,
        Other
    }
}

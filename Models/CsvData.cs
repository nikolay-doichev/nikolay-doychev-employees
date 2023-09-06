using CsvHelper.Configuration.Attributes;

namespace SirmaWebApp.Models
{
    public class CsvData
    {
        [Name("EmpID")]
        public int EmployeeId { get; set; }

        [Name("ProjectID")]
        public int ProjectId { get; set; }

        [Name("DateFrom")]
        public DateTime? DateFrom { get; set; }

        [Name("DateTo")]
        public DateTime? DateTo { get; set; }
    }
}

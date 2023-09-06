using CsvHelper.Configuration;
using SirmaWebApp.Models;

namespace SirmaWebApp.Helpers
{
    public class CsvDataMap : ClassMap<CsvData>
    {
        public CsvDataMap()
        {
            Map(m => m.EmployeeId).Name("EmpID");
            Map(m => m.ProjectId).Name(" ProjectID");
            Map(m => m.DateFrom).Name(" DateFrom");

            // Use the custom date converter for "DateTo" property
            Map(m => m.DateTo).Name(" DateTo").TypeConverter<FlexibleDateTimeConverter>();
        }
    }
}

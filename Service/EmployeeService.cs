using CsvHelper.Configuration;
using CsvHelper;
using SirmaWebApp.Models;
using System.Globalization;
using SirmaWebApp.Helpers;

namespace SirmaWebApp.Service
{
    public class EmployeeService
    {
        public EmployeeService()
        {

        }

        public async Task<IEnumerable<CsvData>> ProcessCsvFile(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                throw new ArgumentNullException("The file is empty or there is not such file");
            }

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<CsvDataMap>();

                IEnumerable<CsvData> records = csv.GetRecords<CsvData>().ToList();

                return records;

                // Now 'records' contains the data from the uploaded CSV file
                // You can process, validate, and save this data as needed
            }
        }
    }
}

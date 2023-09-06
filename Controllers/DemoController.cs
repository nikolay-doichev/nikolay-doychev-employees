using Microsoft.AspNetCore.Mvc;
using SirmaWebApp.Models;
using SirmaWebApp.Service;

namespace SirmaWebApp.Controllers
{
    [Route("demo")]
    public class DemoController : Controller
    {
        private readonly EmployeeService _employeeService;

        public DemoController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add()
        {
            IFormFile file = Request.Form.Files[0]; // Assuming only one file is uploaded

            var csvData = await _employeeService.ProcessCsvFile(file);

            IEnumerable<Employee> employeePairs = csvData
                                    .GroupBy(data => new { data.EmployeeId, data.ProjectId })
                                    .Select(group => new Employee
                                    {
                                        Employee1Id = group.Key.EmployeeId,
                                        Employee2Id = group.Key.ProjectId,
                                        ProjectId = group.Key.ProjectId,
                                        WorkDuration = group.Sum(data => (long)((data.DateTo ?? DateTime.Now) - data.DateFrom).Value.TotalDays)
                                    });

            Employee? longestWorkDurationPair = employeePairs
                                            .OrderByDescending(pair => pair.WorkDuration).FirstOrDefault();

            if (longestWorkDurationPair != null)
            {
                return View(longestWorkDurationPair);
            }
            else
            {
                return BadRequest("No common projects found.");
            }
        }
    }
}

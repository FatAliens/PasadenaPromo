using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasadenaPromo.Server.Contracts.Response;

namespace PasadenaPromo.Server.Controllers
{
    namespace PasadenaPromo.Server.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class EmployeesController(ApplicationContext db) : ControllerBase
        {
            readonly private ApplicationContext _db = db;

            [HttpGet]
            public ActionResult<List<EmployeeListItemResponse>> GetAll()
            {
                var employees = _db.Employees
                    .Include(e => e.User)
                    .Include(e => e.Specialization)
                    .Select(e=>EmployeeListItemResponse.Parse(e))
                    .ToList();

                return employees;
            }
        }
    }
}
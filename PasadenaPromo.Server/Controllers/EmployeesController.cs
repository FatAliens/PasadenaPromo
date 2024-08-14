using Microsoft.AspNetCore.Mvc;

namespace PasadenaPromo.Server.Controllers
{
    namespace PasadenaPromo.Server.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class EmployeeProfileController(ApplicationContext db) : ControllerBase
        {
            readonly private ApplicationContext _db = db;

        }
    }
}
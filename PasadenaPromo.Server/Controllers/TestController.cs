using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasadenaPromo.RepositoryItems;
using PasadenaPromo.Server.RepositoryItems;

namespace PasadenaPromo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(ApplicationContext db) : ControllerBase
    {
        private readonly ApplicationContext _db = db;
        [HttpGet("users")]
        public ActionResult<List<UserDbo>> GetUsers()
        {
            return _db.Users.ToList();
        }
        [HttpGet("roles")]
        public ActionResult<List<RoleDbo>> GetRoles()
        {
            return _db.Roles.ToList();
        }
        [HttpGet("employees")]
        public ActionResult<List<EmployeeDbo>> GetEmployees()
        {
            return _db.Employees
                .Include(e => e.User)
                .ThenInclude(e => e.Role)
                .Include(e => e.WorkingWeek)
                .Include(e => e.Specialization)
                .ToList();
        }
        [HttpGet("specializations")]
        public ActionResult<List<SpecializationDbo>> GetSpecializations()
        {
            return _db.Specializations.ToList();
        }
        [HttpGet("services")]
        public ActionResult<List<ServiceDbo>> GetServices()
        {
            return _db.Services.ToList();
        }
        [HttpGet("portfolio_items")]
        public ActionResult<List<PortfolioItemDbo>> GetPortfolioItems()
        {
            return _db.PortfolioItems.ToList();
        }
        [HttpGet("working_week_days")]
        public ActionResult<List<WorkingWeekDayDbo>> GetWorkingWeekDays()
        {
            return _db.WorkingWeekDays
                .Include(e => e.WorkingWeek)
                .Include(e => e.WorkingDay)
                .Include(e => e.DayOfWeek)
                .ToList();
        }
        [HttpGet("working_period")]
        public ActionResult<List<WorkingPeriodState>> GetWorkingPeriod()
        {
            return _db.WorkingPeriods.Select(p => p.AsState)
                .ToList();
        }
        [HttpGet("working_day")]
        public ActionResult<List<WorkingDayState>> GetWorkingDay()
        {
            return _db.WorkingDays
                .Include(d => d.WorkingPeriods)
                .Select(d => d.AsState)
                .ToList();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasadenaPromo.RepositoryItems;
using PasadenaPromo.Server.Contracts.Response;
using System.Globalization;

namespace PasadenaPromo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController(ApplicationContext db) : ControllerBase
    {
        readonly private ApplicationContext _db = db;

        [HttpGet]
        [Authorize]
        public ActionResult<UserProfileResponse> GetCurrentUserInfo()
        {
            var cookie = Request.Cookies["X-User-Id"];
            if (cookie == null || int.TryParse(cookie, out int userId) == false)
                return BadRequest("User not found!");

            var result = _db.Users.FirstOrDefault(u => u.Id == userId);
            if (result == null)
                return BadRequest("User not found!");

            var responce = UserProfileResponse.Parse(result);

            responce.Reservations = _db.Reservations
                .Where(r => r.ClientId == userId)
                .Include(r => r.Service)
                .Include(r => r.Employee)
                    .ThenInclude(e => e.User)
                .Select(r => UserReservationResponse.Parse(r))
                .ToList();

            return responce;
        }
    }
}
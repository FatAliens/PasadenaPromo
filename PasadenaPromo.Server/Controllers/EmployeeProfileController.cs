using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PasadenaPromo.Server.Contracts.Response;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace PasadenaPromo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProfileController(ApplicationContext db) : ControllerBase
    {
        readonly private ApplicationContext _db = db;

        [Authorize(Roles = "Employee")]
        [HttpGet]
        public ActionResult<EmployeeProfileResponse> GetCurrentEmployeeInfo()
        {
            var cookie = Request.Cookies["X-User-Id"];
            if (cookie == null || int.TryParse(cookie, out int userId) == false)
                return BadRequest("Employee not found!");

            var employee = _db.Employees
                .Include(e=>e.User)
                .Include(e=>e.Specialization)
                .Include(e=>e.WorkingWeek)
                .FirstOrDefault(e => e.User.Id == userId);

            if (employee == null)
                return BadRequest("Employee not found!");

            var employeeProfileResponse = EmployeeProfileResponse.Parse(employee);

            return employeeProfileResponse;
        }

        [Authorize(Roles = "Employee")]
        [HttpPatch("change_avatar")]
        public async Task<ActionResult> ChangeEmployeeAvatar(IFormFile file)
        {
            var cookie = Request.Cookies["X-User-Id"];
            if (cookie == null || int.TryParse(cookie, out int userId) == false)
                return BadRequest("Employee not found!");

            var employee = _db.Employees
                .Include(e => e.User)
                .FirstOrDefault(e => e.User.Id == userId);

            if (employee == null)
                return BadRequest("Employee not found!");

            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var fileInfo = new FileInfo(file.FileName);
            var fileTypes = new List<string> { ".png", ".jpg", ".jpeg", ".webp", ".bmp", ".gif" };
            if (fileTypes.Contains(fileInfo.Extension) == false || fileInfo.Extension.IsNullOrEmpty())
                return BadRequest("File not an image.");

            var filePath = $"avatars/{Guid.NewGuid()}.jpeg";

            using (var readStream = file.OpenReadStream())
            {
                var image = await Image.LoadAsync(readStream);

                image.Mutate(x => x.Resize(new ResizeOptions()
                {
                    Size = new Size(600, 600),
                    Mode = ResizeMode.Crop
                }));

                using (var fileStream = new FileStream($"./wwwroot/{filePath}", FileMode.CreateNew, FileAccess.Write))
                {
                    await image.SaveAsJpegAsync(fileStream);
                }
            }

            employee.AvatarUrl = filePath;

            _db.Update(employee);
            if (_db.SaveChanges() != 1)
                return BadRequest();
            else
                return Ok();
        }
    }
}
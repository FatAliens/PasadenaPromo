using PasadenaPromo.Auth;
using PasadenaPromo.RepositoryItems;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PasadenaPromo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        JwtTokenCreator jwtCreator,
        ApplicationContext db,
        HashService hasher
        ) : ControllerBase
    {
        private readonly JwtTokenCreator _jwtCreator = jwtCreator;
        private readonly ApplicationContext _db = db;
        private readonly HashService _hasher = hasher;

        public record LoginModel([EmailAddress] string? Email, [Required, StringLength(100, MinimumLength = 6)] string Password);
        public record RegistrationModel(
            [Required, StringLength(100, MinimumLength = 2)] string FirstName,
            [Required, StringLength(100, MinimumLength = 5)] string LastName,
            [Required, MinLength(6), MaxLength(100)] string Password,
            [Required] string AvatarURL,
            [Required] EmailAndProof EmailAndProof
        );

        public record ChangePasswordModel(
            [Required] EmailAndProof EmailAndProof,
            [Required, MinLength(6), MaxLength(100)] string Password
            );

        public record EmailAndProof([Required, EmailAddress] string Email, [Required, Range(1, 9999)] int ProofCode);

        [HttpGet("users")]
        public ActionResult<List<UserState>> GetAllUsers()
        {
            return _db.Users.Select(u => u.ToUserState()).ToList();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginModel model)
        {
            UserDbo? user;

            user = _db.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == model.Email);

            if (user == null) return BadRequest();

            if (_hasher.ValidPassword(model.Password, user.PasswordHash))
            {
                user.RefreshToken = GenerateRefreshToken();

                _db.Users.Update(user);
                if (_db.SaveChanges() != 1)
                    return Unauthorized();

                var token = GenerateJwtByUser(user);
                SetTokerCookiesPair(token, user.RefreshToken, Response);
                SetUserIdCookie(user.Id, Response);

                return Ok();
            }
            else return BadRequest();
        }

        [HttpPost("registration")]
        public ActionResult Registration([FromBody] RegistrationModel model)
        {
            //Uniq Email
            if (_db.Users.Any(u => u.Email == model.EmailAndProof.Email))
                return Conflict("email");

            //Proof Code
            //if (_emailProof.ValidateProofCode(model.EmailAndProof.EmailAddress, model.EmailAndProof.ProofCode))
            //    return Unauthorized();

            //Uniq Name
            if (_db.Users.Any(u => u.FirstName == model.FirstName && u.LastName == model.LastName))
                return Conflict("name");

            //Hash Password
            var passwordHash = _hasher.HashPassword(model.Password).ToString();

            //Create new user
            var newUser = new UserDbo
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                AvatarUrl = model.AvatarURL,
                Email = model.EmailAndProof?.Email,
                PasswordHash = passwordHash,
                RefreshToken = GenerateRefreshToken(),
                RoleId = 1,
                Role = _db.Roles.First(r => r.Id == 1)
            };

            //add to db
            _db.Users.Add(newUser);
            if (_db.SaveChanges() != 1)
                return BadRequest();

            _db.Users.Include(u => u.Role);

            var token = GenerateJwtByUser(newUser);
            SetTokerCookiesPair(token, newUser.RefreshToken, Response);
            SetUserIdCookie(newUser.Id, Response);

            return Ok();
        }

        [HttpPatch("change_password")]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            //if (_emailProof.ValidateProofCode(model.EmailAndProof.EmailAddress, model.EmailAndProof.ProofCode) == false)
            //    return Unauthorized();

            var passwordHash = _hasher.HashPassword(model.Password).ToString();

            var user = _db.Users.FirstOrDefault(u => u.Email == model.EmailAndProof.Email);

            if (user == null)
                return BadRequest();

            user.PasswordHash = passwordHash;
            _db.Update(user);
            if (_db.SaveChanges() != 1)
                return BadRequest();
            else
                return Ok();
        }

        [HttpGet("refresh")]
        public ActionResult Refresh()
        {
            var refreshToker = Request.Cookies["X-Refresh-Token"];
            var userIdString = Request.Cookies["X-User-Id"];

            if (refreshToker == null || userIdString == null)
                return Unauthorized();

            if (int.TryParse(userIdString, out var userId) == false)
                return Unauthorized();

            var user = _db.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return Unauthorized();

            if (user.RefreshToken == null || user.RefreshToken != refreshToker)
                return Unauthorized();

            user.RefreshToken = GenerateRefreshToken();

            _db.Users.Update(user);
            if (_db.SaveChanges() != 1)
                return Unauthorized();

            var token = GenerateJwtByUser(user);
            SetTokerCookiesPair(token, user.RefreshToken, Response);
            SetUserIdCookie(user.Id, Response);

            return Ok();
        }

        private string GenerateJwtByUser(UserDbo user)
        {
            return _jwtCreator.Generate(user.Id, user.Role.Title);
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        private void SetTokerCookiesPair(string jwtToken, string refreshToken, HttpResponse response)
        {
            var secureCookieOption = new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict };
            Response.Cookies.Append("X-Access-Token", jwtToken, secureCookieOption);
            Response.Cookies.Append("X-Refresh-Token", refreshToken, secureCookieOption);
        }

        private void SetUserIdCookie(int userId, HttpResponse response)
        {
            var secureCookieOption = new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict };
            response.Cookies.Append("X-User-Id", userId.ToString(), secureCookieOption);
        }
    }
}
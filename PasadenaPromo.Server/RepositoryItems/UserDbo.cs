using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PasadenaPromo.RepositoryItems
{
    [Table("Users")]
    public class UserDbo
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string AvatarUrl { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public int RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public RoleDbo Role { get; set; } = null!;

        public UserState ToUserState()
        {
            return new UserState(
                Id: this.Id,
                FirstName: this.FirstName,
                LastName: this.LastName,
                Avatar: this.AvatarUrl,
                Email: this.Email
                );
        }
    }

    public record UserState(int Id, string FirstName, string LastName, string Avatar, string? Email);
}
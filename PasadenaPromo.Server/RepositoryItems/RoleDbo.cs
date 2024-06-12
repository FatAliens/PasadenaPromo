using System.ComponentModel.DataAnnotations.Schema;

namespace PasadenaPromo.RepositoryItems
{
    [Table("Roles")]
    public class RoleDbo
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public List<UserDbo> Users { get; set; } = null!;
    }
}

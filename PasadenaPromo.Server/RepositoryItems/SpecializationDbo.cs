using System.ComponentModel.DataAnnotations.Schema;

namespace PasadenaPromo.RepositoryItems
{
    [Table("Specializations")]
    public class SpecializationDbo
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
    }
}

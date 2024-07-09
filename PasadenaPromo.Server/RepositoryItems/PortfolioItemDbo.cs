using System.ComponentModel.DataAnnotations.Schema;

namespace PasadenaPromo.RepositoryItems
{
    public class PortfolioItemDbo
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public EmployeeDbo Employee { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? PictureUrl { get; set; }
    }
}

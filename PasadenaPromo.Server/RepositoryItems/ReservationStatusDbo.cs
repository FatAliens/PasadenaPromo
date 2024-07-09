using System.ComponentModel.DataAnnotations.Schema;

namespace PasadenaPromo.RepositoryItems
{
    [Table("ReservationStatuses")]
    public class ReservationStatusDbo
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
    }
}

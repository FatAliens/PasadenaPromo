using PasadenaPromo.Server.RepositoryItems;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasadenaPromo.RepositoryItems
{
    [Table("Reservations")]
    public class ReservationDbo
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate {  get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public EmployeeDbo Employee { get; set; } = null!;
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public ReservationStatusDbo Status { get; set; } = null!;
        public int ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public UserDbo Client { get; set; } = null!;
        public int ServiceId { get; set; }
        [ForeignKey(nameof(ServiceId))]
        public ServiceDbo Service { get; set; } = null!;
        public string ClientPhone { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}

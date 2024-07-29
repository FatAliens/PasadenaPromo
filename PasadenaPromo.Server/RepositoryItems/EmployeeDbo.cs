using PasadenaPromo.Server.RepositoryItems;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasadenaPromo.RepositoryItems
{
    [Table("Employees")]
    public class EmployeeDbo
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        [ForeignKey(nameof(UserId))]
        public UserDbo User {  get; set; } 
        public int WorkingWeekId { get; set; }
        [ForeignKey(nameof(WorkingWeekId))]
        public WorkingWeekDbo WorkingWeek { get; set; }
        public int SpecializationId { get; set; }
        [ForeignKey(nameof(SpecializationId))]
        public SpecializationDbo Specialization { get; set; }
        public string AvatarUrl { get; set; } = null!;
    }
}

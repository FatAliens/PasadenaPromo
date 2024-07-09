using System.ComponentModel.DataAnnotations.Schema;

namespace PasadenaPromo.Server.RepositoryItems
{
    [Table("DaysOfWeek")]
    public class DayOfWeekDbo
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
    }
}

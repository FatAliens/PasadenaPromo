using System.ComponentModel.DataAnnotations.Schema;

namespace PasadenaPromo.Server.RepositoryItems
{
    [Table("WorkingWeekDays")]
    public class WorkingWeekDayDbo
    {
        public int Id { get; set; }
        public int WeekId { get; set; }
        [ForeignKey(nameof(WeekId))]
        public WorkingWeekDbo WorkingWeek { get; set; } = null!;
        public int DayId { get; set; }
        [ForeignKey(nameof(DayId))]
        public WorkingDayDbo WorkingDay { get; set; } = null!;
        public int DayOfWeekId { get; set; }
        [ForeignKey(nameof(DayOfWeekId))]
        public DayOfWeekDbo DayOfWeek { get; set; } = null!;

    }
}

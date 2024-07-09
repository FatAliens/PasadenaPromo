using System.ComponentModel.DataAnnotations.Schema;

namespace PasadenaPromo.Server.RepositoryItems
{
    [Table("WorkingPeriods")]
    public class WorkingPeriodDbo
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public List<WorkingDayDbo> WorkingDays { get; set; } = new List<WorkingDayDbo>();

        public WorkingPeriodState AsState => new WorkingPeriodState(Id, Title, StartTime, EndTime);
    }
    public record WorkingPeriodState(int Id, string Title, TimeSpan StartTime, TimeSpan EndTime);
}

namespace PasadenaPromo.Server.RepositoryItems
{
    public class WorkingDayDbo
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public List<WorkingPeriodDbo> WorkingPeriods { get; set; } = new List<WorkingPeriodDbo>();

        public WorkingDayState AsState => new WorkingDayState(Id, Title, WorkingPeriods.Select(p => p.AsState).ToList());
    }

    public record WorkingDayState(int Id, string Title, List<WorkingPeriodState> WorkingPeriods);
}
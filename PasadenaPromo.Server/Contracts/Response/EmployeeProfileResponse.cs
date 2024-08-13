using PasadenaPromo.RepositoryItems;

namespace PasadenaPromo.Server.Contracts.Response
{
    public class EmployeeProfileResponse
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AvatarUrl { get; set; } = null!;
        public string SpecializationTitle {  get; set; } = null!;
        public string WorkingWeekTitle { get; set; } = null!;

        public static EmployeeProfileResponse Parse(EmployeeDbo employee)
        {
            return new EmployeeProfileResponse
            {
                FirstName = employee.User.FirstName,
                LastName = employee.User.LastName,
                Email = employee.User.Email,
                AvatarUrl = employee.AvatarUrl,
                SpecializationTitle = employee.Specialization.Title,
                WorkingWeekTitle = employee.WorkingWeek.Title
            };
        }
    }
}

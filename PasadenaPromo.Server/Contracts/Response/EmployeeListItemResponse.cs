using PasadenaPromo.RepositoryItems;

namespace PasadenaPromo.Server.Contracts.Response
{
    public class EmployeeListItemResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string AvatarUrl { get; set; } = null!;
        public int SpecializationId { get; set; }
        public string SpecializationTitle { get; set; } = null!;

        public static EmployeeListItemResponse Parse(EmployeeDbo employee)
        {
            return new EmployeeListItemResponse
            {
                Id = employee.Id,
                FirstName = employee.User.FirstName,
                LastName = employee.User.LastName,
                AvatarUrl = employee.AvatarUrl,
                SpecializationId = employee.SpecializationId,
                SpecializationTitle = employee.Specialization.Title
            };
        }
    }
}
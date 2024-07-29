using PasadenaPromo.RepositoryItems;

namespace PasadenaPromo.Server.Contracts.Response
{
    public class UserReservationResponse
    {
        public int StatusCode { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Cost { get; set; }
        public string PictureUrl { get; set; } = null!;
        public string EmployeeName { get; set; } = null!;
        public string EmployeeAvatarUrl { get; set; } = null!;

        public static UserReservationResponse Parse(ReservationDbo reservation)
        {
            var service = reservation.Service;
            var employee = reservation.Employee;
            return new UserReservationResponse
            {
                StatusCode = reservation.StatusId,
                Date = reservation.Date,
                Title = service.Title,
                Description = service.Description,
                Cost = service.Cost,
                PictureUrl = service.PictureUrl,
                EmployeeName = $"{employee.User.FirstName} {employee.User.LastName}",
                EmployeeAvatarUrl = employee.AvatarUrl
            };
        }
    }
}
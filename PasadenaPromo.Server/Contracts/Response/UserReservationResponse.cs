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
            return new UserReservationResponse
            {
                StatusCode = reservation.StatusId,
                Date = reservation.Date,
                Title = reservation.Service.Title,
                Description = reservation.Service.Description,
                Cost = reservation.Service.Cost,
                PictureUrl = reservation.Service.PictureUrl,
                EmployeeName = reservation.Employee.User.FirstName,
                EmployeeAvatarUrl = reservation.Employee.AvatarUrl
            };
        }
    }
}
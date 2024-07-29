using PasadenaPromo.RepositoryItems;

namespace PasadenaPromo.Server.Contracts.Response
{
    public class UserProfileResponse
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<UserReservationResponse> Reservations { get; set; }

        public static UserProfileResponse Parse(UserDbo user)
        {
            return new UserProfileResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Reservations = []
            };
        }
    }
}

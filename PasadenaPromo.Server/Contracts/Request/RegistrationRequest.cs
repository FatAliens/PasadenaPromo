using static PasadenaPromo.Controllers.AuthController;
using System.ComponentModel.DataAnnotations;

namespace PasadenaPromo.Server.Contracts.Request
{
    public record RegistrationRequest(
            [Required, StringLength(100, MinimumLength = 2)] string FirstName,
            [Required, StringLength(100, MinimumLength = 5)] string LastName,
            [Required, MinLength(6), MaxLength(100)] string Password,
            [Required] string AvatarURL,
            [Required] EmailProofRequest EmailAndProof
        );
}
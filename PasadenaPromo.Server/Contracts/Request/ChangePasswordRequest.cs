using System.ComponentModel.DataAnnotations;

namespace PasadenaPromo.Server.Contracts.Request
{
    public record ChangePasswordRequest(
            [Required] EmailProofRequest EmailProofRequest,
            [Required, MinLength(6), MaxLength(100)] string Password
            );
}

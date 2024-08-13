using System.ComponentModel.DataAnnotations;

namespace PasadenaPromo.Server.Contracts.Request
{
    public record ChangeEmailRequest(
        [Required] EmailProofRequest EmailProofRequest
        );
}
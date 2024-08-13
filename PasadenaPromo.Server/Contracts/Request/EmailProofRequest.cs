using System.ComponentModel.DataAnnotations;

namespace PasadenaPromo.Server.Contracts.Request
{
    public record EmailProofRequest(
        [Required, EmailAddress] string Email,
        [Required, Range(1, 9999)] int ProofCode
        );
}
using System.ComponentModel.DataAnnotations;

namespace PasadenaPromo.Server.Contracts.Request
{
    public record LoginRequest(
        [EmailAddress] string? Email,
        [Required, StringLength(100, MinimumLength = 6)] string Password
        );
}
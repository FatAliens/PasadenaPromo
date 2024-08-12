using System.ComponentModel.DataAnnotations;

namespace PasadenaPromo.Server.Contracts.Request
{
    public record ChangeNameRequest(
        [Required, StringLength(100, MinimumLength = 2)] string FirstName,
        [Required, StringLength(100, MinimumLength = 5)] string LastName
        );
}
using System.ComponentModel.DataAnnotations;

namespace ObjectRanking.Models.Dto;

public record LoginRequest(
    [Required] string Email,
    [Required] string Password);
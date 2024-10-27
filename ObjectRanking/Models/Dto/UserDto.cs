using System.ComponentModel.DataAnnotations;

namespace ObjectRanking.Models.Dto;

public record UserDto(
    [Required]string Name,
    [Required]string Email,
    [Required]string Password
    );
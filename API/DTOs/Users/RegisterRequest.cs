using System.ComponentModel.DataAnnotations;
using API.DTOs.Users.Validations;

namespace API.DTOs.Users;

public class RegisterRequest
{
    private string? _firstname;
    private string? _lastname;

    [Required(ErrorMessage = "{0} is required.")]
    [MinLength(3, ErrorMessage = "{0} should be at least 3 characters.")]
    public string? Firstname { get => _firstname; set => _firstname = value?.Trim(); }

    [Required(ErrorMessage = "{0} is required.")]
    [MinLength(3, ErrorMessage = "{0} should be at least 3 characters.")]
    public string? Lastname { get => _lastname; set => _lastname = value?.Trim(); }

    [Required(ErrorMessage = "{0} is required.")]
    [Gender(ErrorMessage = "{0} should either be 'male' or 'female'.")]
    public string? Gender { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Not a valid phonenumber")]
    public string? Phonenumber { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [EmailAddress(ErrorMessage = "{0} is invalid")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [MinLength(3, ErrorMessage = "{0} should be at least 3 characters.")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [MinLength(3, ErrorMessage = "{0} should be at least 3 characters.")]
    public string? City { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [MinLength(3, ErrorMessage = "{0} should be at least 3 characters.")]
    public string? Country { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [MinLength(6, ErrorMessage = "{0} should be at least 6 characters.")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Confirm password is required.")]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
    public string? ConfirmPassword { get; set; }
}
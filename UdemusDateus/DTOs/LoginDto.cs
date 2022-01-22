using System.ComponentModel.DataAnnotations;

namespace UdemusDateus.DTOs;

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}
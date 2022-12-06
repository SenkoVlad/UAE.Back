namespace UAE.Application.Models.User;

public sealed record CreateUserModel(
    string Email,
    string Password);

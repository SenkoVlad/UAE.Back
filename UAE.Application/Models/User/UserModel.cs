using System;

namespace UAE.Application.Models.User;

public sealed record UserModel(string Email, DateTime LastLoginDateTime);

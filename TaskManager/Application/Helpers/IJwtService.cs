using System.Security.Claims;
using TaskManager.Models;

namespace TaskManager.Application.Helpers;

public interface IJwtService
{
    string GenerateJwtToken(UserEntity user);
    ClaimsPrincipal ValidateToken(string token);
}
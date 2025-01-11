using OnlineStore.Domain.Model.User.User;
using System.Security.Claims;

namespace OnlineStore.Application.Extensions
{
    public static class UserExtensions
    {
        public static int GetUserById(this ClaimsPrincipal claims)
        {
            string? userId=claims.Claims.FirstOrDefault(u=>u.Type==ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrWhiteSpace(userId))
            {
                return int.Parse(userId);
            }
            else
            {
                return default;
            }
         
        }   
        public static string GetUserByUserName(this ClaimsPrincipal claims)
        {
            string? userName=claims.Claims.FirstOrDefault(u=>u.Type==ClaimTypes.Name)?.Value;
            if (!string.IsNullOrWhiteSpace(userName))
            {
                return userName;
            }
            else
            {
                return default;
            }
         
        } 
        public static string GetUserByMobile(this ClaimsPrincipal claims)
        {
            string? mobile=claims.Claims.FirstOrDefault(u=>u.Type==ClaimTypes.MobilePhone)?.Value;
            if (!string.IsNullOrWhiteSpace(mobile))
            {
                return mobile;
            }
            else
            {
                return default;
            }
         
        }

        public static string GetFullName(this User user)
        {
            return $"{user?.FirstName} {user?.LastName}";
        }
    }
}

using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;

public static class JwtHelper
{
    public static string GetJwtTokenFromCookies(HttpContext httpContext)
    {
        var jwt = httpContext.Request.Cookies["jwt"]!;
        return jwt;
    }

    public static bool ValidateJwtToken(string token, out JwtSecurityToken jwtSecurityToken, string secretKey)
    {
        jwtSecurityToken = null;

        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            jwtSecurityToken = tokenHandler.ReadJwtToken(token)!;
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            tokenHandler.ValidateToken(token, validationParameters, out _);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Diagnostics;
using GraphFlix.DTOs;

namespace GraphFlix.Services
{
    public class AuthProcessor
    {
        private const string _jwtPublicKey = "TestCertificateAndJwtKomNuForHelvedeHvorMangeTegnSkalDerTil"; // Move to appsettings or environment variable
        private const string _url = "https://localhost:7172"; // Move to appsettings or environment variable
        private const int _tokenValidity = 60; // Minutes

        public static TokenDto Generate()
        {
            byte[] tokenKey = GetTokenKey();
            DateTime tokenExpiresTime = SetTokenExpiry();
            SecurityTokenDescriptor securityTokenDescriptor = GetDescriptor(GetRoleClaim(), tokenExpiresTime, tokenKey);
            return AssignTokenProperties(securityTokenDescriptor, tokenExpiresTime);
        }

        private static byte[] GetTokenKey()
        {
            try
            {
                return Encoding.UTF8.GetBytes(_jwtPublicKey);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        private static DateTime SetTokenExpiry()
        {
            try
            {
                return DateTime.Now.AddMinutes(_tokenValidity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        private static SecurityTokenDescriptor GetDescriptor(List<Claim> claims, DateTime tokenExpiresTime, byte[] tokenKey)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = _url,
                Issuer = _url,
                Expires = tokenExpiresTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };
        }

        private static List<Claim> GetRoleClaim()
        {
            // Get role from db instead
            return new List<Claim> { new Claim(ClaimTypes.Role, "Admin") };
        }

        private static TokenDto AssignTokenProperties(SecurityTokenDescriptor securityTokenDescriptor, DateTime tokenExpiresTime)
        {
            TokenDto newToken = new TokenDto();
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            try
            {
                newToken.TokenString = jwtSecurityTokenHandler.WriteToken(securityToken);
                newToken.ExpiresIn = (int)tokenExpiresTime.Subtract(DateTime.Now).TotalSeconds;
                return newToken;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Diagnostics;
using GraphFlix.DTOs;

<<<<<<<< HEAD:GraphFlix - API/Services/AuthProcessor.cs
namespace GraphFlix.Services
{
    public class AuthProcessor
    {
        private const string _jwtPublicKey = "TestCertificateAndJwtKomNuForHelvedeHvorMangeTegnSkalDerTil"; // Move to appsettings or environment variable
        private const string _url = "https://localhost:7172"; // Move to appsettings or environment variable
        private const int _tokenValidity = 60; // Minutes

        public static TokenDto Generate()
========
namespace GraphFlix.Processors
{
    public class AuthProcessor
    {
        public static Token Generate()
>>>>>>>> e8ca53db6f4c73c3388798bb9d7aa2a04f926fab:GraphFlix - API/Processors/AuthProcessor.cs
        {
            byte[] tokenKey = GetTokenKey();
            DateTime tokenExpiresTime = SetTokenExpiry();
            List<Claim> claims = new List<Claim> { GetRoleClaim(), GetUserIdClaim() };
            SecurityTokenDescriptor securityTokenDescriptor = GetDescriptor(claims, tokenExpiresTime, tokenKey);
            return AssignTokenProperties(securityTokenDescriptor, tokenExpiresTime);
        }

        private static byte[] GetTokenKey()
        {
            try
            {
                return Encoding.UTF8.GetBytes(GetAppSettings("JwtKey"));
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
                return DateTime.Now.AddMinutes(Convert.ToDouble(GetAppSettings("JwtValidityInMinutes")));
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
                Audience = GetAppSettings("JwtUrl"),
                Issuer = GetAppSettings("JwtUrl"),
                Expires = tokenExpiresTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };
        }

<<<<<<<< HEAD:GraphFlix - API/Services/AuthProcessor.cs
        private static List<Claim> GetRoleClaim()
========
        private static Claim GetRoleClaim()
>>>>>>>> e8ca53db6f4c73c3388798bb9d7aa2a04f926fab:GraphFlix - API/Processors/AuthProcessor.cs
        {
            // Get role from db instead
            return new Claim(ClaimTypes.Role, "Admin");
        }
        private static Claim GetUserIdClaim()
        {
            // Get user id from db instead
            return new Claim("UserId", "0");
        }

<<<<<<<< HEAD:GraphFlix - API/Services/AuthProcessor.cs
        private static TokenDto AssignTokenProperties(SecurityTokenDescriptor securityTokenDescriptor, DateTime tokenExpiresTime)
========
        private static Token AssignTokenProperties(SecurityTokenDescriptor securityTokenDescriptor, DateTime tokenExpiresTime)
>>>>>>>> e8ca53db6f4c73c3388798bb9d7aa2a04f926fab:GraphFlix - API/Processors/AuthProcessor.cs
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

        private static string GetAppSettings(string property)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
            IConfigurationRoot config = builder.Build();
            return config[property];
        }
    }
}

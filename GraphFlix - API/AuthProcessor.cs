using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using GraphFlix.Models;
using System.Diagnostics;

namespace GraphFlix
{
    public class AuthProcessor
    {
        private const string _jwtPublicKey = "TestCertificateAndJwtKomNuForHelvedeHvorMangeTegnSkalDerTil"; // Move to appsettings or environment variable
        private const string _url = "https://localhost:27016";
        private const int _tokenValidity = 60; // Minutes

        public Token Generate()
        {
            byte[] tokenKey = GetTokenKey();
            DateTime tokenExpiresTime = SetTokenExpiry();
            SecurityTokenDescriptor securityTokenDescriptor = GetDescriptor(GetRoleClaim(), tokenExpiresTime, tokenKey);
            return AssignTokenProperties(securityTokenDescriptor, tokenExpiresTime);
        }

        private byte[] GetTokenKey()
        {
            try
            {
                return Encoding.ASCII.GetBytes(_jwtPublicKey);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        private DateTime SetTokenExpiry()
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

        private SecurityTokenDescriptor GetDescriptor(List<Claim> claims, DateTime tokenExpiresTime, byte[] tokenKey)
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

        private List<Claim> GetRoleClaim()
        {
            // Get role from db instead
            return new List<Claim> { new Claim(ClaimTypes.Role, "User") };
        }

        private Token AssignTokenProperties(SecurityTokenDescriptor securityTokenDescriptor, DateTime tokenExpiresTime)
        {
            Token newToken = new Token();
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

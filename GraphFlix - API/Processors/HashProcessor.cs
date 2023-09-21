using GraphFlix.Services;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace GraphFlix.Processors
{
    public class HashProcessor : IHashingService
    {
        public string GetHash(string input)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
            IConfigurationRoot config = builder.Build();

            try
            {
                int numberOfIterations = Convert.ToInt32(config["HashIterations"]);
                byte[] tempInput = Encoding.UTF8.GetBytes(input);
                byte[] tempValue = new byte[1];

                SHA512 sha = SHA512.Create();

                for (int i = 0; i < numberOfIterations; i++)
                {
                    if (i == 0)
                    {
                        tempValue = sha.ComputeHash(tempInput);
                    }
                    else
                    {
                        tempValue = sha.ComputeHash(tempValue);
                    }
                }

                return Convert.ToBase64String(sha.ComputeHash(tempValue));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        public string HashPassword(string password, string salt)
        {
            int insertIndex = password.Length / 2;
            string passwordSalt = password.Insert(insertIndex, salt);
            return GetHash(passwordSalt);
        }
    }
}

using System.Diagnostics;
using System.Security.Cryptography;

namespace GraphFlix.Processors
{
    public class SaltProcessor
    {
        public string GenerateSalt()
        {
            try
            {
                return new Guid().ToString().Replace("-", "").Substring(0, 16);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
    }
}

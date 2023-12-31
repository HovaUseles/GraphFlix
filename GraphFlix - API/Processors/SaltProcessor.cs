﻿using GraphFlix.Services;
using System.Diagnostics;
using System.Security.Cryptography;

namespace GraphFlix.Processors
{
    public class SaltProcessor : ISaltService
    {
        public string GenerateSalt()
        {
            try
            {
                return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
    }
}

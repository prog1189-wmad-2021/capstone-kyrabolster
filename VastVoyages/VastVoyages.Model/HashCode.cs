using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace VastVoyages.Model
{
    public class HashCode
    {
        public string CalculateSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            UTF8Encoding objUtf8 = new UTF8Encoding();
            byte[] hashValue = sha256.ComputeHash(objUtf8.GetBytes(str));
            StringBuilder returnValue = new StringBuilder();

            for (int i = 0; i < hashValue.Length; i++)
            {
                returnValue.Append(hashValue[i].ToString());
            }

            return returnValue.ToString();
        }
    }
}

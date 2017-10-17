using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WorldShoes.Helpers
{
    public class GeradorMD5
    {
        public static string GetMd5HashData(string senha)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(senha)).Select(s => s.ToString("x2")));
        }
    }
}
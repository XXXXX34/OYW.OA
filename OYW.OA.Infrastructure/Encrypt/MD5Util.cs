using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace OYW.OA.Infrastructure.Encrypt
{
    public class MD5Util
    {
        public static string GetMD5(String str)
        {
            string pwd = string.Empty;
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            for (int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString("x");
            }
            return pwd;
        }
    }
}

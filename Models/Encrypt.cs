using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace graduation_project.Models
{
    public class Encrypt
    {
        public static string GetMD5Hash(string input)
        {
            //Encrypt text using md5
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] b = System.Text.Encoding.UTF8.GetBytes(input);
                b = md5.ComputeHash(b);//Hash data
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (byte x in b)
                    sb.Append(x.ToString("x2"));//hexadecimal
                return sb.ToString();
            }
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TattoAPI.Repository.Lib
{
    public class OTPGenerator
    {
        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public string Encode(string encodeMe)
        {
            string strencodeMe = encodeMe + "_" + "1";
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeMe);
            string encode = Convert.ToBase64String(encoded);
            return encode;
        }

        public string Decode(string decodeMe)
        {
            byte[] encoded = null;
            try
            {
                
                encoded = Convert.FromBase64String(decodeMe);
                string strencodeMe = System.Text.Encoding.UTF8.GetString(encoded);
                string Decode = strencodeMe.Split('_')[0];


                return Decode;
            }
            catch (Exception ex)
            {
                return "00";
            }
        }
    }
}
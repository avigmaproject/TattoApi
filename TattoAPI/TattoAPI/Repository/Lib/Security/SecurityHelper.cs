using System;
using System.Data;
using System.Configuration;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
using System.Text;
using System.Security.Cryptography;

/// <Buildsummary>
/// Copyright InteractCRM, 2007 - 2008
/// Summary description for Context
/// /// All rights are reserved. Reproduction or transmission in whole or in part, 
/// in any form or by any means, electronic, mechanical or otherwise, is 
/// prohibited without the prior written consent of the copyright owner.
/// Filename: SecurityHelper.cs
/// Description:
/// Build Number: 10
///Last Updated on:
/// Last Updated by: 
/// Modification Details
/// Modified on:
/// Modified by:
/// Description:
/// </Buildsummary>



/// <summary>
/// <author> niral </author>
/// </summary>
namespace TattoAPI.Repository.Lib.Security
{
    public class SecurityHelper
    {

        static byte key1;
        public SecurityHelper()
        {

        }


        public string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray = null;
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file


            string key = (string)settingsReader.GetValue("SecurityKey", typeof(string));

            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            //tdes.Key = keyArray;
            tdes.Key = Encoding.ASCII.GetBytes("84563215874235gg96541257");
            tdes.IV = Encoding.ASCII.GetBytes("88A5TEVE");


            //tdes.GenerateKey();
            //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string
            string strcipherString = Uri.UnescapeDataString(cipherString);
            byte[] toEncryptArray = Convert.FromBase64String(strcipherString);

            AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("SecurityKey", typeof(string));

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = Encoding.ASCII.GetBytes("84563215874235gg96541257");
            tdes.IV = Encoding.ASCII.GetBytes("88A5TEVE");

            // tdes.GenerateKey();
            //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return Encoding.UTF8.GetString(resultArray);
        }

        public string GetMD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public string Encode(string encodeMe)
        {
            byte[] encoded = Encoding.UTF8.GetBytes(encodeMe);
            return Convert.ToBase64String(encoded);
        }

        public string Decode(string decodeMe)
        {
            byte[] encoded = null;
            try
            {
                encoded = Convert.FromBase64String(decodeMe);
                return Encoding.UTF8.GetString(encoded);
            }
            catch (Exception ex)
            {
                return "00";
            }
        }
    }
}
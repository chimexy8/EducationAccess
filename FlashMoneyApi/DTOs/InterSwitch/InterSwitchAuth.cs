using crypto;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs.InterSwitch
{
    public class InterSwitchAuth
    {
        protected static string publicKeyExponent = "010001";
        protected static string publicKeyModulus = "009C7B3BA621A26C4B02F48CFC07EF6EE0AED8E12B4BD11C5CC0ABF80D5206BE69E1891E60FC88E2D565E2FABE4D0CF630E318A6C721C3DED718D0C530CDF050387AD0A30A336899BBDA877D0EC7C7C3FFE693988BFAE0FFBAB71B25468C7814924F022CB5FDA36E0D2C30A7161FA1C6FB5FBD7D05ADBEF7E68D48F8B6C5F511827C4B1C5ED15B6F20555AFFC4D0857EF7AB2B5C18BA22BEA5D3A79BD1834BADB5878D8C7A4B19DA20C1F62340B1F7FBF01D2F2E97C9714A9DF376AC0EA58072B2B77AEB7872B54A89667519DE44D0FC73540BEEAEC4CB778A45EEBFBEFE2D817A8A8319B2BC6D9FA714F5289EC7C0DBC43496D71CF2A642CB679B0FC4072FD2CF";


        public static string getAuthData(string pan, string pin, string expiryDate, string cvv, string mod = null, string pubExpo = null)
        {
            string authData = GetAuthData(pan, pin, expiryDate, cvv, mod, pubExpo);
            return authData;
        }

        private static string GetAuthData(string pan, string pin, string expiryDate, string cvv2, string modulus, string pubExpo)
        {
            if (pan != null)
            {
                pan = pan.Trim();
            }
            else
            {
                pan = "";
            }
            if (pan != null)
            {
                pan = pan.Trim();
            }
            else
            {
                pan = "";
            }
            if (cvv2 != null)
            {
                cvv2 = cvv2.Trim();
            }
            else
            {
                cvv2 = "";
            }
            if (expiryDate != null)
            {
                expiryDate = expiryDate.Trim();
            }
            else
            {
                expiryDate = "";
            }

            if (modulus != null)
                publicKeyModulus = modulus;

            if (pubExpo != null)
                publicKeyExponent = pubExpo;

            string authData = string.Format("1Z{0}Z{1}Z{2}Z{3}", pan, pin, expiryDate, cvv2);
            string result = RsaEncryptWithPrivate(authData);
            return result;
        }

        private static string RsaEncryptWithPrivate(string clearText)
        {
            BigInteger Mod = new BigInteger(publicKeyModulus, 16);
            //static BigInteger Mod = new BigInteger(Encoding.UTF8.GetBytes(modulus));
            BigInteger PubExp = new BigInteger(publicKeyExponent, 16);

            var bytesToEncrypt = Encoding.UTF8.GetBytes(clearText);
            RsaKeyParameters pubParameters = new RsaKeyParameters(false, Mod, PubExp);
            Pkcs1Encoding encryptEngine = new Pkcs1Encoding(new RsaEngine());
            encryptEngine.Init(true, pubParameters);
            var encrypted = Convert.ToBase64String(encryptEngine.ProcessBlock(bytesToEncrypt, 0, bytesToEncrypt.Length));
            return encrypted;
        }
    }
}

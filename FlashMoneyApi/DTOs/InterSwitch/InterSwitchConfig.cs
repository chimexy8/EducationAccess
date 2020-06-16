using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs.InterSwitch
{
    public class InterSwitchConfig
    {
        //public string Authorizations;
        public static readonly String Sha1 = "Sha1";
        private String clientID;
        private String secretKey;
        private String HTTPVerb;
        private String url;
        private String accessToken;
        public string SignedParameters { get; private set; }
        public string Nonce { get; private set; }
        public string PasportAuthorization { get; private set; }
        public string TimeStamp { get; set; }
        public string Authorization { get; private set; }
        public string Signature { get; set; }
        public static SecureRandom Random
        {
            get { return _random; }
            set { _random = value; }
        }

        private static SecureRandom _random = new SecureRandom();

        public long GetTimeStamp()
        {
            return (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public string GetAuthorization()
        {
            Authorization = "Bearer " + accessToken;
            return Authorization;
        }

        public string GetNonce()
        {
            //char[] chars = new char[62];
            //chars ="1234567890".ToCharArray();
            //byte[] data = new byte[1];
            //RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            //crypto.GetNonZeroBytes(data);
            //data = new byte[15];
            //crypto.GetNonZeroBytes(data);
            //StringBuilder result = new StringBuilder(15);
            //foreach (byte b in data)
            //{
            //    result.Append(chars[b % (chars.Length)]);
            //}
            //return result.ToString();

            Guid uuid = Guid.NewGuid();
            String nonce = uuid.ToString();
            nonce = nonce.Replace("-", "");
            return nonce;
        }

        public string GetSignature()
        {
            StringBuilder signature = new StringBuilder(HTTPVerb);
            signature.Append("&")
                .Append(Uri.EscapeDataString(url))
                .Append("&")
                .Append(TimeStamp)
                .Append("&")
                .Append(Nonce)
                .Append("&")
                .Append(clientID)
                .Append("&")
                .Append(secretKey);

            if (SignedParameters != null && !SignedParameters.Equals(""))
            {
                signature.Append("&")
                .Append(SignedParameters);
            }
            return ComputeHash(signature.ToString());
        }

        public static string ComputeHash(string input)
        {
            var data = Encoding.UTF8.GetBytes(input);
            Sha1Digest hash = new Sha1Digest();
            hash.BlockUpdate(data, 0, data.Length);
            byte[] result = new byte[hash.GetDigestSize()];
            hash.DoFinal(result, 0);
            return Convert.ToBase64String(result);
        }

        public InterSwitchConfig(string httpVerb, string url, string clientId, string secretKey, string accessToken, string signedParameters = null)
        {
            HTTPVerb = httpVerb;
            this.url = url;
            this.clientID = clientId;
            this.secretKey = secretKey;
            this.accessToken = accessToken;
            this.SignedParameters = signedParameters;
            TimeStamp = GetTimeStamp().ToString();
            Nonce = GetNonce();
            Authorization = GetAuthorization();
            Signature = GetSignature();

        }

        public InterSwitchConfig()
        {

        }
    }
}

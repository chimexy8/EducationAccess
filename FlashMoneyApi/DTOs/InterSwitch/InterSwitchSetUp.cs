using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs.InterSwitch
{
    public class InterSwitchSetUp
    {
        public string clientId;
        public string clientSecret;
        public string myAccessToken;
        public string environment;
        public string authData;
        public static string SANDBOX = "SANDBOX";
        public static string PRODUCTION = "PRODUCTION";
        public static string DEV = "DEVELOPMENT";
        public static string HTTP_CODE = "CODE";
        public static string HTTP_RESPONSE = "RESPONSE";

        public InterSwitchSetUp(string clientId, string clientSecret, string environment = null)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.environment = environment;

        }
        public String getPassportUrl(string env)
        {
            if (env == null)
            {
                return Constants.SANDBOX_URL;//default to sandbox
            }
            if (env.Equals(PRODUCTION, StringComparison.OrdinalIgnoreCase))
            {
                return Constants.PRODUCTION_URL;
            }
            else if (env.Equals(SANDBOX, StringComparison.OrdinalIgnoreCase))
            {
                return Constants.SANDBOX_URL;
            }
            else if (env.Equals(DEV, StringComparison.OrdinalIgnoreCase))
            {
                return "https://qa.interswitchng.com";
            }
            else
            {
                return null;
            }
        }


        public async Task<Token> GetClientAccessToken(String ClientId, String ClientSecret)
        {
            string url = getPassportUrl(environment);

            url = string.Concat(url, "/passport/oauth/token");

            using (HttpClient _client = new HttpClient())
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Add(Constants.Authorization, setAuthorization(ClientId, ClientSecret));
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type","client_credentials"),
                };

                var content = new FormUrlEncodedContent(pairs);

                var responseMessage = await _client.PostAsync(url, content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<Token>(result);
                    json.setAccessToken(json.access_token);
                    return json;
                }
            }
            return null;
        }


        //public Dictionary<string, string> Send(string uri, string httpMethod, object data = null, Dictionary<string, string> headers = null, String signedParameters = null)
        //{
        //    try
        //    {
        //        Token token = GetClientAccessToken(this.clientId, this.clientSecret).Result;
        //        var accessToken = token.access_token;
        //        return SendWithAccessToken(uri, httpMethod, accessToken, data, headers, signedParameters);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //public Dictionary<string, string> SendWithAccessToken(string uri, string httpMethod, string accessToken, object data = null, Dictionary<string, string> headers = null, string signedParameters = null)
        //{
        //    try
        //    {


        //        string url = getUrl(environment);
        //        url = String.Concat(url, uri);

        //        RestClient client = new RestClient(url);
        //        client.IgnoreResponseStatusCode = true;
        //        IRestResponse response = null;
        //        InterSwitchConfig authConfig = new InterSwitchConfig(httpMethod, url, this.clientId, this.clientSecret, accessToken, signedParameters);

        //        Method httpMethodObj = (httpMethod == null || httpMethod.Equals("")) ? Method.GET : new Method(httpMethod);

        //        var paymentRequests = new RestRequest(url, httpMethodObj);
        //        paymentRequests.AddHeader(Constants.Contenttype, "application/json");
        //        paymentRequests.AddHeader("Signature", authConfig.Signature);
        //        paymentRequests.AddHeader("SignatureMethod", "SHA1");
        //        paymentRequests.AddHeader("Timestamp", authConfig.TimeStamp);
        //        paymentRequests.AddHeader("Nonce", authConfig.Nonce);
        //        paymentRequests.AddHeader("Authorization", authConfig.Authorization);
        //        if (headers != null && headers.Count() > 0)
        //        {
        //            foreach (KeyValuePair<string, string> entry in headers)
        //            {
        //                paymentRequests.AddHeader(entry.Key, entry.Value);
        //            }
        //        }

        //        if (data != null)
        //            paymentRequests.AddJsonBody(data);


        //        ServicePointManager.Expect100Continue = true;
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //        JsonDeserializer deserial = new JsonDeserializer();
        //        //try
        //        //{
        //        response = client.Execute(paymentRequests);
        //        /*
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.StackTrace.ToString());
        //            throw ex;
        //        }
        //        */

        //        HttpStatusCode httpStatusCode = response.StatusCode;
        //        int numericStatusCode = (int)httpStatusCode;
        //        Dictionary<string, string> responseObject = new Dictionary<string, string>();
        //        responseObject.Add(HTTP_CODE, numericStatusCode.ToString());
        //        responseObject.Add(HTTP_RESPONSE, System.Text.Encoding.UTF8.GetString(response.RawBytes));

        //        return responseObject;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public string GetAuthData(string pan, string expiryDate, string cvv, string pin, string mod = null, string pubExpo = null)
        {
            authData = InterSwitchAuth.getAuthData(pan, pin, expiryDate, cvv, mod, pubExpo);
            return authData;
        }



        private static string setAuthorization(string clientId, string clientSecret)
        {
            try
            {
                string Auth;
                byte[] bytes;
                bytes = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", clientId, clientSecret));
                Auth = Convert.ToBase64String(bytes);
                return string.Concat("Basic ", Auth);
            }
            catch (Exception e)
            {
                throw new Exception("Unable to encode parameters, Please contact connect@interswitchng.com. for assistance.", e);
            }
        }

        public static string getUrl(String environment)
        {
            string url = Constants.SANDBOX_URL;
            if (PRODUCTION.Equals(environment, StringComparison.OrdinalIgnoreCase))
            {
                url = Constants.PRODUCTION_URL;
            }
            else if (DEV.Equals(environment, StringComparison.OrdinalIgnoreCase))
            {
                url = Constants.DEVELOPMENT_URL;
            }
            return url;
        }

    }

    public class Token
    {

        public string access_token { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public string expires_in { get; set; }
        public string scope { get; set; }
        public string requestor_id { get; set; }
        public string merchant_code { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string payable_id { get; set; }
        public string payment_code { get; set; }
        public string jti { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public void setAccessToken(string token)
        {
            this.access_token = token;
        }
    }
    public class Error1
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class Error2
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class ErrorResponse
    {
        public List<Error1> errors { get; set; }
        public Error2 error { get; set; }
        public string transactionRef { get; set; }
    }

    public class Constants
    {
        public static string SANDBOX_URL = "https://sandbox.interswitchng.com";
        public static string PRODUCTION_URL = "https://saturn.interswitchng.com";
        public static string DEVELOPMENT_URL = "https://qa.interswitchng.com";

        public static string Contenttype = "content-type";
        public static string Cachecontrol = "cache-control";
        public static string Authorization = "Authorization";
        public static string ContentType = "application/x-www-form-urlencoded";

        public static String CARD_NAME = "default";
        public static String SECURE_HEADER = "4D";
        //public static String SECURE_FORMAT_VERSION = "11";
        public static String SECURE_FORMAT_VERSION = "12";
        public static String SECURE_MAC_VERSION = "05";
        public static String SECURE_FOOTER = "5A";
        public static String SIGNATURE_HEADER = "Signature";
        public static String SIGNATURE_METHOD_HEADER = "SignatureMethod";

    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;
using Anecka.Labs;

namespace Anecka.Labs.Convio
{
    public class ConvioBase
    {
        protected string API_KEY = ConfigurationManager.AppSettings["CONVIO_API_KEY"];
        protected string LOGIN_NAME = ConfigurationManager.AppSettings["CONVIO_LOGIN_NAME"]; 
        protected string LOGIN_PASS = ConfigurationManager.AppSettings["CONVIO_LOGIN_PASS"]; 
        protected string VERSION = ConfigurationManager.AppSettings["CONVIO_VERSION"]; 

        public string BASE_URL = ConfigurationManager.AppSettings["CONVIO_BASE_URL"]; 
        public string BASE_URL2 = ConfigurationManager.AppSettings["CONVIO_BASE_URL2"]; 

        protected Dictionary<string,string> parameters = new Dictionary<string,string>();

        protected ConvioBase()
        {
            parameters.Add("api_key", API_KEY);
            parameters.Add("login_name", LOGIN_NAME);
            parameters.Add("login_password", LOGIN_PASS);
            parameters.Add("v", VERSION);
            parameters.Add("response_format", "xml");
            parameters.Add("suppress_result_codes", "true");
        }

        protected void AddParameter(string key, string value)
        {
            if (!parameters.ContainsKey(key))
                parameters.Add(key, value);
            else
                parameters[key] = value;
        }

        protected string BuildParameters()
        {
            StringBuilder sb = new StringBuilder();

            if (parameters.Count > 0)
            {
                int count = 0;
                foreach (string key in parameters.Keys)
                {
                    if (count > 0)
                        sb.Append("&");
                    sb.Append(string.Format("{0}={1}", key, parameters[key]));
                    count++;
                }
            }

            return sb.ToString();
        }

        protected T GetResponse<T>(string convioAPI, string rootXmlElement)
        {
            return GetResponse<T>(BASE_URL, "GET", convioAPI, rootXmlElement);
        }

        protected T GetResponse<T>(string method, string convioAPI, string rootXmlElement)
        {
            return GetResponse<T>(BASE_URL2, "POST", convioAPI, rootXmlElement);
        }

        protected T GetResponse<T>(string baseUrl, string method, string convioAPI, string rootXmlElement)
        {
            string url = string.Empty; // = string.Format("{0}{1}?", BASE_URL, convioAPI);
            //url += BuildParameters();

            WebRequest webRequest; //= HttpWebRequest.Create(url);
            
            if (method.ToUpper() == "POST")
            {
                url = string.Format("{0}{1}?", baseUrl, convioAPI);
                //url = string.Format("{0}{1}?", BASE_URL2, convioAPI);
                //url += BuildParameters();

                webRequest = HttpWebRequest.Create(url);
                webRequest.Method = method;
                webRequest.ContentType = "application/x-www-form-urlencoded";
                //webRequest.Credentials = new NetworkCredential(LOGIN_NAME, LOGIN_PASS);

                byte[] postData = Encoding.UTF8.GetBytes(BuildParameters());
                webRequest.ContentLength = postData.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(postData, 0, postData.Length);
                stream.Close();
            }
            else
            {
                //url = string.Format("{0}{1}?", BASE_URL, convioAPI);
                url = string.Format("{0}{1}?", baseUrl, convioAPI);
                url += BuildParameters();
                webRequest = HttpWebRequest.Create(url);
                webRequest.Method = method;
            }

            string buffer = string.Empty;

            try
            {
                WebResponse response = webRequest.GetResponse();
                using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    buffer = sr.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
#if DEBUG             
                string message = ex.Message;
                WebResponse errorResponse = ex.Response;
                if (errorResponse != null)
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(errorResponse.GetResponseStream()))
                    {
                        buffer = sr.ReadToEnd();
                    }

                    Utility.WriteErrorLog(ex, url + "\n" + BuildParameters() + "\n" + buffer);
                }
                else
                {
                    Utility.WriteErrorLog(ex.InnerException, url + "\n" + BuildParameters() + "\n" + message);
                }
                
#endif
                return default(T);
            }

            return XMLHelper.DeserializeList<T>(buffer, rootXmlElement, @"http://convio.com/crm/v1.0");
        }
    }
}

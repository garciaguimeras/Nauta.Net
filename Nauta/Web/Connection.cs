using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nauta.Web
{

    class Connection
    {

        public static async Task<string[]> Get(string url, string proxy, string formContent)
        {
            try
            {
                HttpClientHandler handler = null;
                if (!string.IsNullOrEmpty(proxy))
                {
                    handler = new HttpClientHandler()
                    {
                        Proxy = new WebProxy(proxy),
                        UseProxy = true,
                    };
                }
                string result = "";
                HttpClient client = new HttpClient(handler);

                url = url + "?" + formContent;
                var response = await client.GetAsync(url);
                result = await response.Content.ReadAsStringAsync();

                return result.Split('\n');
            }
            catch (Exception)
            { }

            return new string[] { };
        }

        public static async Task<string[]> Post(string url, string proxy, Dictionary<string, string> formContent)
        {
            try
            {
                HttpClientHandler handler = null;
                if (!string.IsNullOrEmpty(proxy))
                {
                    handler = new HttpClientHandler()
                    {
                        Proxy = new WebProxy(proxy),
                        UseProxy = true,
                    };
                }
                string result = "";
                HttpClient client = new HttpClient(handler);

                List<KeyValuePair<string, string>> form = new List<KeyValuePair<string, string>>();
                foreach (string key in formContent.Keys)
                {
                    form.Add(new KeyValuePair<string, string>(key, formContent[key]));
                }

                var content = new FormUrlEncodedContent(form);
                var response = await client.PostAsync(new Uri(url), content);
                result = await response.Content.ReadAsStringAsync();

                return result.Split('\n');
            }
            catch (Exception)
            { }

            return new string[] { };
        }

    }
}

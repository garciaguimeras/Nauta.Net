using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nauta.Web
{

    class Connection
    {

        public static async Task<string[]> Get(string url, string proxy, string formContent)
        {
            try
            {
                HttpClient client = new HttpClient();
                if (!string.IsNullOrEmpty(proxy))
                {
                    var handler = new HttpClientHandler()
                    {
                        Proxy = new WebProxy(proxy),
                        UseProxy = true,
                    };
                    client = new HttpClient(handler);
                }

                string result = "";
               
                url = url + "?" + formContent;
                var response = await client.GetAsync(url);
                result = await response.Content.ReadAsStringAsync();

                return result.Split('\n');
            }
            catch (Exception e)
            {
                // MessageBox.Show(e.Message);
            }

            return new string[] { };
        }

        public static async Task<string[]> Post(string url, string proxy, Dictionary<string, string> formContent)
        {
            try
            {
                HttpClient client = new HttpClient();
                if (!string.IsNullOrEmpty(proxy))
                {
                    var handler = new HttpClientHandler()
                    {
                        Proxy = new WebProxy(proxy),
                        UseProxy = true,
                    };
                    client = new HttpClient(handler);
                }

                string result = "";

                List<KeyValuePair<string, string>> form = new List<KeyValuePair<string, string>>();
                foreach (string key in formContent.Keys)
                {
                    form.Add(new KeyValuePair<string, string>(key, formContent[key]));
                }

                var content = new FormUrlEncodedContent(form);
                var response = await client.PostAsync(url, content);
                result = await response.Content.ReadAsStringAsync();

                return result.Split('\n');
            }
            catch (Exception e)
            {
                // MessageBox.Show(e.Message);
            }

            return new string[] { };
        }

    }
}

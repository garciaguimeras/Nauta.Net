using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nauta.Web
{
    class Actions
    {
        string proxy;
        string username;
        string password;

        public Actions(string username, string password, string proxyServer, string proxyPort)
        {
            this.username = username;
            this.password = password;
            this.proxy = "";
            if (!string.IsNullOrEmpty(proxyServer) && !string.IsNullOrEmpty(proxyPort))
            {
                this.proxy = "http://" + proxyServer + ":" + proxyPort;
            }
        }

        public async Task<Dictionary<string, string>> HomePage()
        {
            var response = await Connection.Get(@"https://secure.etecsa.net:8443", this.proxy, "");

            var parser = new ResponseParser();
            var dict = parser.ParseHomeResponse(response);
            if (!dict.ContainsKey("gotopage"))
                dict.Add("gotopage", "/nauta_hogar/LoginURL/pc_login.jsp");
            if (!dict.ContainsKey("successpage"))
                dict.Add("successpage", "/nauta_hogar/OnlineURL/pc_index.jsp");
            if (!dict.ContainsKey("lang"))
                dict.Add("lang", "es_ES");
            if (!dict.ContainsKey("ssid"))
                dict.Add("ssid", "nauta_hogar");
            else
                dict["ssid"] = "nauta_hogar";

            dict.Add("username", this.username);
            dict.Add("password", this.password);

            return dict;
        }

        public async Task<LoginResponse> Login(Dictionary<string, string> form)
        {
            var response = await Connection.Post(@"https://secure.etecsa.net:8443/LoginServlet", this.proxy, form);

            var parser = new ResponseParser();
            return parser.ParseLoginResponse(response);
        }

        public async Task<string> GetAvailableTime(string loginParams)
        {
            loginParams = "op=getLeftTime&" + loginParams;
            var response = await Connection.Post(@"https://secure.etecsa.net:8443/EtecsaQueryServlet", this.proxy, loginParams);
            return response != null && response.Count() > 0 ? response[0] : null;
        }

        public async Task<bool> Logout(string loginParams)
        {
            loginParams = loginParams + "&remove=1";
            var response = await Connection.Post(@"https://secure.etecsa.net:8443/LogoutServlet", this.proxy, loginParams);
            var parser = new ResponseParser();
            return parser.ParseLogoutResponse(response);
        }

    }
}

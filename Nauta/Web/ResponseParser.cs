using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Nauta.Web
{

    public class LoginResponse
    {
        public string session { get; set; }
        public string loggerId { get; set; }
        public string ssid { get; set; }
        public string domain { get; set; }
        public string username { get; set; }
        public string wlanacname { get; set; }
        public string wlanmac { get; set; }
        public string wlanuserip { get; set; }


        public bool AlreadyConnected { get; set; }
        public bool NoMoney { get; set; }
        public bool BadUsername { get; set; }
        public bool BadPassword { get; set; }

        public string GetParamString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (session != null) stringBuilder.Append(session);
            if (loggerId != null) stringBuilder.Append(loggerId);
            if (ssid != null) stringBuilder.Append(ssid);
            if (domain != null) stringBuilder.Append(domain);
            if (username != null) stringBuilder.Append(username);
            if (wlanuserip != null) stringBuilder.Append(wlanuserip);
            if (wlanmac != null) stringBuilder.Append(wlanmac);
            if (wlanacname != null) stringBuilder.Append(wlanacname);
            return stringBuilder.ToString();
        }
    }

    class ResponseParser
    {

        public Dictionary<string, string> ParseHomeResponse(string[] response)
        {
            string pattern1 = "<input type=\"hidden\" name=\"([A-Za-z0-9./]*)\" id=\"[A-Za-z0-9./]*\" value=\"([A-Za-z0-9./]*)\"";
            string pattern2 = "<input type='hidden' name='CSRFHW' value='([A-Za-z0-9]*)'";

            var dict = new Dictionary<string, string>();
            foreach (string resp in response)
            {
                var line = resp.Trim();
                var match = Regex.Match(line, pattern1, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    Console.WriteLine("{0} --> {1}", match.Groups[1], match.Groups[2]);
                    dict.Add(match.Groups[1].ToString(), match.Groups[2].ToString());
                }

                match = Regex.Match(line, pattern2, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    if (!dict.ContainsKey("CSRFHW"))
                    {
                        Console.WriteLine("CSRFHW --> {0}", match.Groups[1]);
                        dict.Add("CSRFHW", match.Groups[1].ToString());
                    }
                }
            }
            return dict;
        }

        public LoginResponse ParseLoginResponse(string[] response)
        {
            string pattern3 = "alert\\(\"Su tarjeta no tiene saldo disponible";
            string pattern4 = "alert\\(\"El usuario ya está conectado.\"\\);";
            string pattern5 = "alert\\(\"Entre el nombre de usuario y contraseña correctos";
            string pattern6 = "alert\\(\"No se pudo autorizar al usuario";

            string pattern10 = "var urlParam = \"([A-Za-z0-9=_&]*)\"";
            string pattern11 = "\\+ \"(&loggerId=[0-9]*\\+[A-Za-z0-9._@]*)\"";
            string pattern12 = "\\+ \"(&ssid=[A-Za-z0-9._@]*)\"";
            string pattern13 = "\\+ \"(&domain=[A-Za-z0-9._@]*)\"";
            string pattern14 = "\\+ \"(&username=[A-Za-z0-9._@]*)\"";
            string pattern15 = "\\+ \"(&wlanacname=[A-Za-z0-9._@]*)\"";
            string pattern16 = "\\+ \"(&wlanmac=[A-Za-z0-9._@]*)\"";
            string pattern17 = "\\+ \"(&wlanuserip=[A-Za-z0-9._@]*)\"";

            string session = null;
            string loggerId = null;
            string ssid = null;
            string domain = null;
            string username = null;
            string wlanacname = null;
            string wlanmac = null;
            string wlanuserip = null;

            bool alreadyConnected = false;
            bool noMoney = false;
            bool badPassword = false;
            bool badUsername = false;

            foreach (string resp in response)
            {
                var line = resp.Trim();
                Console.WriteLine(line);

                var match = Regex.Match(line, pattern10, RegexOptions.IgnoreCase);
                if (session == null && match.Success)
                {
                    Console.WriteLine("{0}", match.Groups[1]);
                    session = match.Groups[1].ToString();
                }

                match = Regex.Match(line, pattern11, RegexOptions.IgnoreCase);
                if (loggerId == null && match.Success)
                {
                    Console.WriteLine("{0}", match.Groups[1]);
                    loggerId = match.Groups[1].ToString();
                }

                match = Regex.Match(line, pattern12, RegexOptions.IgnoreCase);
                if (ssid == null && match.Success)
                {
                    Console.WriteLine("{0}", match.Groups[1]);
                    ssid = match.Groups[1].ToString();
                }

                match = Regex.Match(line, pattern13, RegexOptions.IgnoreCase);
                if (domain == null && match.Success)
                {
                    Console.WriteLine("{0}", match.Groups[1]);
                    domain = match.Groups[1].ToString();
                }

                match = Regex.Match(line, pattern14, RegexOptions.IgnoreCase);
                if (username == null && match.Success)
                {
                    Console.WriteLine("{0}", match.Groups[1]);
                    username = match.Groups[1].ToString();
                }

                match = Regex.Match(line, pattern15, RegexOptions.IgnoreCase);
                if (wlanacname == null && match.Success)
                {
                    Console.WriteLine("{0}", match.Groups[1]);
                    wlanacname = match.Groups[1].ToString();
                }

                match = Regex.Match(line, pattern16, RegexOptions.IgnoreCase);
                if (wlanmac == null && match.Success)
                {
                    Console.WriteLine("{0}", match.Groups[1]);
                    wlanmac = match.Groups[1].ToString();
                }

                match = Regex.Match(line, pattern17, RegexOptions.IgnoreCase);
                if (wlanuserip == null && match.Success)
                {
                    Console.WriteLine("{0}", match.Groups[1]);
                    wlanuserip = match.Groups[1].ToString();
                }

                // Boolean checks
                match = Regex.Match(line, pattern3, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    Console.WriteLine("{0}", match.Groups[0]);
                    noMoney = true;
                }

                match = Regex.Match(line, pattern4, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    Console.WriteLine("{0}", match.Groups[0]);
                    alreadyConnected = true;
                }

                match = Regex.Match(line, pattern5, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    Console.WriteLine("{0}", match.Groups[0]);
                    badPassword = true;
                }

                match = Regex.Match(line, pattern6, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    Console.WriteLine("{0}", match.Groups[0]);
                    badUsername = true;
                }
            }

            if (session == null && loggerId == null && !alreadyConnected && !noMoney && !badPassword && !badUsername)
                return null;

            return new LoginResponse
            {
                session = session,
                loggerId = loggerId,
                ssid = ssid,
                domain = domain,
                username = username,
                wlanuserip = wlanuserip,
                wlanmac = wlanmac,
                wlanacname = wlanacname,

                AlreadyConnected = alreadyConnected,
                NoMoney = noMoney,
                BadPassword = badPassword,
                BadUsername = badUsername
            };
        }

        public bool ParseLogoutResponse(string[] response)
        {
            if (response == null || response.Length == 0)
                return false;

            var line = response[0].Trim();
            Console.WriteLine(line);
            return line.Equals("logoutcallback('SUCCESS');");
        }

    }

}

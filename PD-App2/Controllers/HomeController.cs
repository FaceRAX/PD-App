using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PD_App2.Models;
using System.Diagnostics;
using System.Net;

namespace PD_App2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private InfografiaContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, InfografiaContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            string randomQuote = FetchResponse(ApiFetcher.Endpoints.Quotes.random(), true);
            string randomChar = FetchResponse(ApiFetcher.Endpoints.Characters.random(), true);
            string deaths = FetchResponse(ApiFetcher.Endpoints.Deaths.count(), true);
            var data = (JArray)JsonConvert.DeserializeObject(randomQuote);
            var dataC = (JArray)JsonConvert.DeserializeObject(randomChar);
            var dataD = (JArray)JsonConvert.DeserializeObject(deaths);
            string quote_id = string.Empty;
            string quote = string.Empty;
            string author = string.Empty;
            Character c = new Character();
            int dc = 0;
            foreach (var data0 in data)
            {
                quote_id = data0["quote_id"].Value<string>();
                quote = data0["quote"].Value<string>();
                author = data0["author"].Value<string>();
            }

            foreach (var data0 in dataD)
            {
                dc = data0["deathCount"].Value<int>();
            }

            foreach (var data0 in dataC)
            {
                c.char_id = Int32.Parse(data0["char_id"].Value<string>());
                c.name = data0["name"].Value<string>();
                c.imgUrl = data0["img"].Value<string>();
                c.status = data0["status"].Value<string>();
                c.nickname = data0["nickname"].Value<string>();
                c.portrayed = data0["portrayed"].Value<string>();
            }
            c.appearance = String.Empty;
            c.occupation = String.Empty;
            Quote quoteOb = new Quote();
            quoteOb.quoteid = Int32.Parse(quote_id); ;
            quoteOb.quote = quote;
            quoteOb.author = author;
            quoteOb.series = "Breaking Bad";

            Infografia info = new Infografia();
            info.data = randomQuote;
            Infografia info1 = new Infografia();
            info1.data = randomChar;
            context.quotes.Add(quoteOb);
            context.Infografias.Add(info);
            context.Infografias.Add(info1);
            context.SaveChanges();
            ViewBag.qtData = quote;
            ViewBag.qtAuthor = author;

            ViewBag.cname = c.name;
            ViewBag.cimgUrl = c.imgUrl;
            ViewBag.cstatus = c.status;
            ViewBag.cnickname = c.nickname;
            ViewBag.cportrayed = c.portrayed;
            ViewBag.dc = dc;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        protected static class ApiFetcher
        {
            private static string baseUrl = "https://www.breakingbadapi.com/{api}";

            public static string urlTOString()
            {
                return baseUrl;
            }

            public static string BuildUpdateUrlToEndpoint(string endp)
            {
                string baseUrl = urlTOString();
                string newUrl = baseUrl.Replace("{api}", endp);
                return newUrl;
            }

            public static Uri BuildUpdateUriToEndpoint(string endp)
            {
                return new Uri(BuildUpdateUrlToEndpoint(endp));
            }

            public static class Endpoints 
            {
                public static class Characters 
                {
                    public static string all() 
                    {
                        return "api/characters";
                    }

                    public static string single(int id)
                    { 
                        string endp = "api/character/{id}";
                        endp.Replace("{id}", id.ToString());
                        return endp;
                    }

                    public static string random() 
                    {
                        return "api/character/random";
                    }
                }

                public static class Episodes
                {
                    public static string all()
                    {
                        return "api/episodes";
                    }

                    public static string single(int id)
                    {
                        string endp = "api/episode/{id}";
                        endp.Replace("{id}", id.ToString());
                        return endp;
                    }
                }

                public static class Quotes
                {
                    public static string all()
                    {
                        return "api/quotes";
                    }

                    public static string single(int id)
                    {
                        string endp = "api/quote/{id}";
                        endp.Replace("{id}", id.ToString());
                        return endp;
                    }

                    public static string random()
                    {
                        return "api/quote/random";
                    }
                }

                public static class Deaths
                {
                    public static string all()
                    {
                        return "api/deaths";
                    }

                    public static string single(int name)
                    {
                        string endp = "api/death?name={name}";
                        endp.Replace("{name}", name.ToString());
                        return endp;
                    }

                    public static string count()
                    {
                        return "api/death-count";
                    }
                    public static string random()
                    {
                        return "api/random-death";
                    }
                }
            }
        }

        private static string FetchResponse(string endp ,bool useURI = false)
        {
            string buildedUrl = string.Empty;
            string response = String.Empty;
            Uri uri = null;
            if (!useURI)
            {
                buildedUrl = ApiFetcher.BuildUpdateUrlToEndpoint(endp);
                response = GetUrl(buildedUrl);
            }
            else
            {
                uri = ApiFetcher.BuildUpdateUriToEndpoint(endp);
                response = GetUrl(uri);
            }
            System.Net.ServicePointManager.Expect100Continue = true;
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            SaveSearch(response);
            return response;
        }

        private static string GetUrl(string url)
        {
            try
            {
                WebRequest request = HttpWebRequest.Create(url);
                request.Timeout = 10000; //Milliseconds
                WebResponse response = GetResponseNoException(request);
                //WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need  
                //Response.Write(urlText.ToString());
                return urlText;
            }
            catch (Exception e) { return string.Empty; }
        }

        private static string GetUrl(Uri uri)
        {
            try
            {
                WebRequest request = HttpWebRequest.Create(uri);
                request.Timeout = 10000; //Milliseconds
                WebResponse response = GetResponseNoException(request);
                //WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need  
                //Response.Write(urlText.ToString());
                return urlText;
            }
            catch (Exception e) { return string.Empty; }
        }

        private static WebResponse GetResponseNoException(WebRequest req)
        {
            try
            {
                return (WebResponse)req.GetResponse();
            }
            catch (WebException we)
            {
                var resp = we.Response as WebResponse;
                if (resp == null)
                    throw;
                return resp;
            }
        }

        private static bool SaveSearch(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                return false;
            }
            else if (response.Contains("\"success\":true"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
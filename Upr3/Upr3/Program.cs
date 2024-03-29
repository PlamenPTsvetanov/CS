using HtmlAgilityPack;
using System;
using System.Data.SqlTypes;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter number for exercise:\n\t1.Whois\n\t2.Current Local Time\n\t3.Scrape data from a Website");
        int command = int.Parse(Console.ReadLine());

        switch (command)
        {
            case 1:
                whois();
                break;
            case 2:
                localTime();
                break;
            case 3:
                scraper();
                break;
        }
    }

    private static async void whois()
    {
        Console.WriteLine("Enter ip address:");
        string ip = Console.ReadLine();

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("User-Agent", "MyUserAgent");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer YourAccessToken");


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ipapi.co/" + ip + "/country/");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                Console.WriteLine(reader.ReadToEnd());
            }
        }
    }

    private static async void localTime()
    {
        using (HttpClient client = new HttpClient())
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.timeanddate.com/worldclock/bulgaria/sofia");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using Stream stream = response.GetResponseStream();
            using StreamReader reader = new StreamReader(stream);
            string htmlContent = reader.ReadToEnd();

            string pattern = @"\d{2}:\d{2}:\d{2}";

            Regex regex = new Regex(pattern);

            Match match = regex.Match(htmlContent);
            if (match.Success)
            {
                Console.WriteLine("Current time is: " + match.Value);
            }
        }
    }

    static async Task scraper()
    {
        Console.WriteLine("Enter filter:");
        string filter = Console.ReadLine();
        using (HttpClient client = new HttpClient())
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.mediapool.bg/news");
            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using Stream stream = response.GetResponseStream();
            using StreamReader reader = new StreamReader(stream);
            {
                string htmlContent = reader.ReadToEnd();

                htmlContent = WebUtility.HtmlDecode(htmlContent);
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlContent);

                List<HtmlNode> articles = htmlDoc.DocumentNode.Descendants()
                    .Where(a => (a.Name == "article" && a.Attributes["id"] != null)).ToList();

                using (StreamWriter outputFile = new StreamWriter(Path.Combine("", "/output.txt")))
                {
                    foreach (var article in articles)
                    {

                        HtmlNode h2 = article.ChildNodes.FindFirst("h2");
                        if (h2 == null || h2.InnerText == null || h2.InnerText.Contains(filter))
                        {
                            continue;
                        }


                        outputFile.WriteLine(h2.InnerText);

                    }
                }

            }
        }
    }
}

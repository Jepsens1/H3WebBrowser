using System.Text.RegularExpressions;

namespace H3WebBrowser
{
    internal class Program
    {
        static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Insert url");
            string userinput = Console.ReadLine();
            await Request(userinput);
            Console.ReadLine();
        }
        static async Task Request(string url)
        {
            try
            {
                //Sends a GET request to the url
                HttpResponseMessage response = await client.GetAsync(url);
                //Ensure that the response is success, otherwise throws exception
                response.EnsureSuccessStatusCode();
                //Gets the content of the response message
                string body = await response.Content.ReadAsStringAsync();
                //Regex to replace all html tags
                //Does not remove script or style tags, still finding a solution
                Regex removetags = new Regex("<(?:\"[^\"]*\"['\"]*|'[^']*'['\"]*|[^'\">])+>");
                string htmlremove = removetags.Replace(body, "");

                Console.WriteLine(htmlremove);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error {e.StackTrace}\n{e.Message}");
            }
        }
    }
}
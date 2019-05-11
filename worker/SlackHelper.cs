using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace worker
{
    public static class SlackHelper
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public static async Task SendSlackMessage(string message)
        {
            await HttpClient.PostAsync("https://hooks.slack.com/services/*******************", // Full url removed for obvious reasons ;)
                new StringContent($@"{{ ""text"": ""{message}"" }}", Encoding.UTF8, "application/json"));
        }
    }
}
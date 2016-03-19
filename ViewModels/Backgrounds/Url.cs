using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace Floodlight.Service.ViewModels.Backgrounds
{
    public class Url {
        public static string[] ValidExtensions = {"jpg", "jpeg", "png", "bmp"};
        
        private readonly string _url;
        
        public Url(string url) {
            _url = url;
            
            ProcessUrl().Wait();
        }
        
        public bool IsValid => ValidExtensions.Any(_url.Contains);

        public string ContentType { get; set; }
        
        public Stream Stream { get; set; }
        
        private async Task ProcessUrl() {
            var httpClientHandler = new HttpClientHandler();
            using (var client = new HttpClient(httpClientHandler)) {
                // Retrieve the URL
                var result = await client.GetAsync(_url);
                if (!result.IsSuccessStatusCode) {
                    var resultStr = await result.Content.ReadAsStringAsync();
                    throw new Exception("Couldn't get image from URL: " + resultStr);
                }
                
                // Fill in the properties needed
                ContentType = result.Content.Headers.ContentType.MediaType;
                Stream = await result.Content.ReadAsStreamAsync();
            }
        }
    }
}
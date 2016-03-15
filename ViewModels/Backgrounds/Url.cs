using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace BGChanger_Server.ViewModels.Backgrounds
{
    public class Url {
        public static string[] VALID_EXTENSIONS = {"jpg", "jpeg", "png", "bmp"};
        
        private string _url;
        
        public Url(string url) {
            _url = url;
        }
        
        public bool IsValid {
            get {
                return VALID_EXTENSIONS.Any(_url.Contains);
            }
        }
        
        public string ContentType { get; set; }
        
        public Stream Stream {
            get {
                return GetStream().Result;
            }
        }
        
        private async Task<Stream> GetStream() {
            var httpClientHandler = new HttpClientHandler();
            using (var client = new HttpClient(httpClientHandler)) {
                var result = await client.GetAsync(_url);
                if (!result.IsSuccessStatusCode) {
                    var resultStr = await result.Content.ReadAsStringAsync();
                    throw new Exception("Couldn't get image from URL: " + resultStr);
                }
                
                ContentType = result.Headers.GetValues("Content-Type").FirstOrDefault();
                
                return await result.Content.ReadAsStreamAsync();
            }
        }
    }
}
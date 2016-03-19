using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floodlight.Service.ViewModels.Backgrounds;
using Microsoft.AspNet.Mvc;
using System.Linq;
using Floodlight.Service.Models;
using Microsoft.AspNet.Http.Extensions;

namespace Floodlight.Service.Controllers {
    [Route("api")]
    public class ApiController : Controller {
        // For Testing Purposes Only
        private Dictionary<string, string> TestRawBackgrounds = new Dictionary<string, string>() {
                    { "fd8975da-e829-4b05-8473-594a446ee79a", "http://i.imgur.com/BtV19gC.jpg" },
                    { "0d8cdf96-9504-4aa5-8a0f-42aef4cfa282", "http://i.imgur.com/5u3FHii.jpg" },
                    { "10b92684-69d2-4ded-a3b9-e121b44aac63", "http://i.imgur.com/zAB9nE7.jpg" },
                    { "efa3c8a2-2830-43d9-bdc2-897cf59c644d", "http://i.imgur.com/BT20rKV.jpg" },
                    { "a17ebb19-adeb-44ba-ae27-119e0bab4f9a", "http://i.imgur.com/D6nRZhH.jpg" }
                };
        
        private Dictionary<string, Background> TestBackgrounds = new Dictionary<string, Background>();
        
        private ApplicationDbContext DbContext { get; }
        
        public ApiController(ApplicationDbContext dbContext) {
            DbContext = dbContext;
                           
            foreach (var url in TestRawBackgrounds) {
                var parsedUrl = new ViewModels.Backgrounds.Url(url.Value);
        
                var bg = new Background() {
                    Id = url.Key,
                    Title = "This is a Test Image with Guid " + url.Key,
                    ContentType = parsedUrl.ContentType,
                };
                
                TestBackgrounds.Add(url.Key, bg);
            }
        }

        [HttpGet ("")]
        public string Index()
        {
            return "Please work oh god please";
        }

        [HttpGet("user/{uid}")]
        public async Task<string> UserDetails(string uid)
        {
            // TODO: Return user details here
            
            /**
            JSON Format
            
            {
                
            }
            
            */
            return "";
        }
        
        [HttpGet("user/{uid}/backgrounds")]
        public IEnumerable<Background> Backgrounds(string uid)
        {
            return TestBackgrounds.Values;
            
            // TODO: Check this works\
            //return DbContext.Backgrounds.Where(bg => bg.User.Id == uid).Select(bg => bg.ToViewModel());
        }
        
        [HttpGet("background/{bgid}")]
        public Background Background(string bgid)
        {            
            return TestBackgrounds[bgid];
            
            // TODO: Check this works
            //return _dbContext.Backgrounds.FirstOrDefault(bg => bg.Id.ToString() == bgid).Select(bg => bg.ToViewModel());
        }

        [HttpGet("background/{bgid}/image")]
        public FileResult Image(string bgid)
        {
            var url = new ViewModels.Backgrounds.Url(TestRawBackgrounds[bgid]);

            return new FileStreamResult(url.Stream, url.ContentType);

            // TODO: Code this for realzies yo
        }
    }
}
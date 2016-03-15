using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BGChanger_Server.ViewModels.Backgrounds;
using Microsoft.AspNet.Mvc;
using System.Linq;
using BGChanger_Server.Models;

namespace BGChanger_Server.BGChanger_Server.Controllers {
    [Route("api")]
    class ApiController {
        private ApplicationDbContext _dbContext { get; set; }
        
        public ApiController(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
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
        
        [HttpGet("backgrounds/{uid}")]
        public IEnumerable<Background> Backgrounds(string uid)
        {
            // TODO short: Return a hardcoded list of backgrounds here
            // TODO: Return a list of backgrounds here
            
            /**
            JSON Format
            
            {
                
            }
            
            */
            return _dbContext.Backgrounds.Where(bg => bg.User.Id == uid).Select(bg => bg.ToViewModel());
        }
        
        [HttpGet("background/{bgid}")]
        public Background Background(string bgid)
        {            
            var url = new ViewModels.Backgrounds.Url("http://i.imgur.com/BtV19gC.jpg");
            
            var bg = new Background() {
                Title = "test image",
                ContentType = url.ContentType,
                Height = 1080,
                Width = 1920,
                FileStream = url.Stream
            };
            
            return bg;
            //return _dbContext.Backgrounds.FirstOrDefault(bg => bg.Id.ToString() == bgid).Select(bg => bg.ToViewModel());
        }
    }
}
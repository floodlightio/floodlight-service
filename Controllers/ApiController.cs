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
                    { "a17ebb19-adeb-44ba-ae27-119e0bab4f9a", "http://i.imgur.com/D6nRZhH.jpg" },
                    { "35198e49-1a32-4de6-b75a-9b3f82130995", "http://i.imgur.com/dntWXbq.jpg" },
                    { "a1c5a68c-8088-4434-b8c5-7834df127580", "http://i.imgur.com/3q20Zzi.jpg" },
                    { "6d4bef5e-a6e5-43b2-9cfd-f94593ab0d04", "http://i.imgur.com/YK0RJHk.jpg" },
                    { "799e5daf-d820-4bd3-9355-f8d526b63e6c", "http://i.imgur.com/NbghAku.jpg" },
                    { "36b409ed-2d21-46d5-ab37-e3866cf254a5", "http://i.imgur.com/XL0xDis.jpg" },
                    { "f896778c-310e-4a72-b475-3da22971a3df", "http://i.imgur.com/pC2j3Rs.jpg" },
                    { "5000648b-ff52-489b-8950-b8086f1ce2d6", "http://i.imgur.com/PhH6Obr.jpg" },
                    { "336d27a9-9388-4dfe-9cd5-cf2eb065e6fa", "http://i.imgur.com/48HePS3.jpg" },
                    { "55e6dd24-7423-49e4-8dcc-ff9037b3f2df", "http://i.imgur.com/M4ckcal.jpg" },
                    { "58c6fb6c-a12f-4001-b3ac-b4c9528e5e66", "http://i.imgur.com/yuHIbkq.jpg" },
                    { "e86b630d-2384-4ea3-bb31-673407469854", "http://i.imgur.com/2fkftqq.jpg" },
                    { "d953b1cd-676d-4101-8d7e-2b719738e4e8", "http://i.imgur.com/YiX2SkZ.jpg" },
                    { "56a4f972-9423-454a-9dca-381dc9e129a7", "http://i.imgur.com/GL6AY84.jpg" },
                    { "d9dba03d-f8bb-47ff-9608-a322cd82ebeb", "http://i.imgur.com/00NEvNx.jpg" },
                    { "03f855a2-895e-4c8b-aca5-9cef4c70ae9c", "http://i.imgur.com/1zMInER.jpg" },
                    { "da0b0c92-a507-4a47-8bae-ed8b845c8bc5", "http://i.imgur.com/UfypKLS.jpg" },
                    { "cfbb00c7-3953-4682-8574-b18c2ac6367c", "http://i.imgur.com/6SwghIx.jpg" },
                };
                
        private Dictionary<string, string> TestBackgroundTitles = new Dictionary<string, string>() {
                    { "fd8975da-e829-4b05-8473-594a446ee79a", "Nigardsbreen, Norway" },
                    { "0d8cdf96-9504-4aa5-8a0f-42aef4cfa282", "Torres Del Paine National Park, Chile" },
                    { "10b92684-69d2-4ded-a3b9-e121b44aac63", "Jones Lake, British Columbia" },
                    { "efa3c8a2-2830-43d9-bdc2-897cf59c644d", "Huacachina, Peru" },
                    { "a17ebb19-adeb-44ba-ae27-119e0bab4f9a", "Keflavík, Iceland" },
                    { "35198e49-1a32-4de6-b75a-9b3f82130995", "Northern Lights" },
                    { "a1c5a68c-8088-4434-b8c5-7834df127580", "Thingvellir National Park, Iceland" },
                    { "6d4bef5e-a6e5-43b2-9cfd-f94593ab0d04", "Laxenburg, Austria" },
                    { "799e5daf-d820-4bd3-9355-f8d526b63e6c", "Bryce Canyon, Utah" },
                    { "36b409ed-2d21-46d5-ab37-e3866cf254a5", "Oneonta Gorge, Oregon" },
                    { "f896778c-310e-4a72-b475-3da22971a3df", "Lake Tahoe, California" },
                    { "5000648b-ff52-489b-8950-b8086f1ce2d6", "Ross Lake, Washington" },
                    { "336d27a9-9388-4dfe-9cd5-cf2eb065e6fa", "Arches National Park, Utah" },
                    { "55e6dd24-7423-49e4-8dcc-ff9037b3f2df", "Monarch Lake, Colorado" },
                    { "58c6fb6c-a12f-4001-b3ac-b4c9528e5e66", "Antelope Island, Utah" },
                    { "e86b630d-2384-4ea3-bb31-673407469854", "Grand Staircase-Escalante, Utah" },
                    { "d953b1cd-676d-4101-8d7e-2b719738e4e8", "Melbourne, Australia" },
                    { "56a4f972-9423-454a-9dca-381dc9e129a7", "Philadelphia, Pennsylvania" },
                    { "d9dba03d-f8bb-47ff-9608-a322cd82ebeb", "Chicago, Illinois" },
                    { "03f855a2-895e-4c8b-aca5-9cef4c70ae9c", "La Paz, Bolivia" },
                    { "da0b0c92-a507-4a47-8bae-ed8b845c8bc5", "Sydney, Australia" },
                    { "cfbb00c7-3953-4682-8574-b18c2ac6367c", "Bangkok, Thailand" },
                };
        
        private Dictionary<string, Background> TestBackgrounds = new Dictionary<string, Background>();
        
        private ApplicationDbContext DbContext { get; }
        
        public ApiController(ApplicationDbContext dbContext) {
            DbContext = dbContext;
                           
            foreach (var url in TestRawBackgrounds) {
                var parsedUrl = new ViewModels.Backgrounds.Url(url.Value);
        
                var bg = new Background() {
                    Id = url.Key,
                    Title = TestBackgroundTitles[url.Key],
                    ContentType = parsedUrl.ContentType,
                };
                
                TestBackgrounds.Add(url.Key, bg);
            }
        }

        /// <summary>
        /// Gets details about the requested user.
        /// </summary>
        /// <param name="uid">User ID to get details for</param>
        /// <returns>A JSON representation of the user's information.</returns>
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
        
        /// <summary>
        /// Get a list of the user's backgrounds.
        /// </summary>
        /// <param name="uid">The User ID to get backrgounds for.</param>
        /// <returns>A JSON representation of all the user's backgrounds.</returns>
        [HttpGet("user/{uid}/backgrounds")]
        public IEnumerable<Background> Backgrounds(string uid)
        {
            return TestBackgrounds.Values;
            
            // TODO: Check this works\
            //return DbContext.Backgrounds.Where(bg => bg.User.Id == uid).Select(bg => bg.ToViewModel());
        }
        
        /// <summary>
        /// Get a single background's metadata.
        /// </summary>
        /// <param name="bgid">The Background ID to search for.</param>
        /// <returns>Metadata about the background, including content type.</returns>
        [HttpGet("background/{bgid}")]
        public Background Background(string bgid)
        {            
            return TestBackgrounds[bgid];
            
            // TODO: Check this works
            //return _dbContext.Backgrounds.FirstOrDefault(bg => bg.Id.ToString() == bgid).Select(bg => bg.ToViewModel());
        }

        /// <summary>
        /// Get the image associated with the background, as stored on the server.
        /// </summary>
        /// <param name="bgid">The Background ID to get the image for.</param>
        /// <returns>The image with no extension but correct content type.</returns>
        /// <remarks>A client should use the ContentType attribute on /backgrounds/{bgid} to construct the final file with extension.</remarks>
        [HttpGet("background/{bgid}/image")]
        public FileResult Image(string bgid)
        {
            var url = new ViewModels.Backgrounds.Url(TestRawBackgrounds[bgid]);

            return new FileStreamResult(url.Stream, url.ContentType);

            // TODO: Code this for realzies yo
        }
    }
}
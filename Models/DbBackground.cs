using System.IO;
using Microsoft.AspNet.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Floodlight.Service.ViewModels.Backgrounds;

namespace Floodlight.Service.Models
{
    public class DbBackground
    {
        public DbBackground(string title, string url, ApplicationUser user) {
            // Set passed in parameters
            Title = title;
            Url = url;
            User = user;
            
            // Get the URL contents and set the rest
            var urlViewModel = new Url(Url);
            ContentType = urlViewModel.ContentType;
            FileStream = urlViewModel.Stream;
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [MaxLength(100)]
        public string Title { get; set; }
        
        [MaxLength(200)]
        public string Url { get; set; }
        
        public Stream FileStream { get; set; }
        
        [MaxLength(100)]
        public string ContentType { get; set; }
        
        public ApplicationUser User { get; set; }
        
        public Background ToViewModel() {
            return new Background() {
                Id = Id.ToString(),
                Title = Title,
                ContentType = ContentType
            };
        }
    }
}

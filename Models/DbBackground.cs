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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [MaxLength(100)]
        public string Title { get; set; }
        
        [MaxLength(100)]
        public string ContentType { get; set; }
        
        public ApplicationUser User { get; set; }
        
        public Stream FileStream {
            get {
                // TODO: Need to rewrite this whole thing to take into account that URLs get contenttype directly
                // Also need to figure out how to do that for files

                if (File != null) {
                    return File.OpenReadStream();
                } else if (Url != null) {
                    return UrlViewModel.Stream;
                } else {
                    throw new NoStreamAvailableException();
                }
            }
        }
        
        [NotMapped]
        public string Url { get; set; }
        
        [NotMapped]
        private Url UrlViewModel {
            get {
                if (Url != null) {
                    return new Url(Url);
                } else {
                    throw new NoUrlAvailableException();
                }
            }
        }
        
        [NotMapped]
        [FileExtensions (Extensions = "jpeg,jpg,png,bmp")]
        public IFormFile File { get; set; }
        
        public Background ToViewModel() {
            return new Background() {
                Id = Id.ToString(),
                Title = Title,
                ContentType = ContentType
            };
        }
    }
    
    public class NoStreamAvailableException : Exception {
    }
    
    public class NoUrlAvailableException : Exception {
    }
}

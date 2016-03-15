using System.IO;
using BGChanger_Server.Models;

namespace BGChanger_Server.ViewModels.Backgrounds
{
    public class Background
    {
        public string Title { get; set; }
        
        public string ContentType { get; set; }
        
        public int Height { get; set; }
        
        public int Width { get; set; }
                
        public Stream FileStream { get; set; }
    }
}

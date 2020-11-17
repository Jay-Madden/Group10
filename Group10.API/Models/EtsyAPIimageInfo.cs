using System.Collections.Generic;


namespace Group10.API.Models
{
    public class EtsyAPIimageInfo
    {
        public int count { get; set; } 
        public List<ImageResult>? results { get; set; } 
        public ImageParams? @params { get; set; } 
        public string? type { get; set; } 
        public ImagePagination? pagination { get; set; } 
    }
    public class ImageResult    {
        public string? url_170x135 { get; set; } 
    }

    public class ImageParams    {
        public string? listing_id { get; set; } 
    }

    public class ImagePagination    {
    }
    

}
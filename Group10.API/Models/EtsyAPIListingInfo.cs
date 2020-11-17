using System.Collections.Generic;

namespace Group10.API.Models
{
    public class EtsyAPIListingInfo
    {
        public int count { get; set; } 
        public List<Result>? results { get; set; } 
        public Params? @params { get; set; } 
        public string? type { get; set; } 
        public Pagination? pagination { get; set; } 
    }
    public class Result    {
        public int listing_id { get; set; } 
        public string? title { get; set; } 
        public string? description { get; set; }
        public string? price { get; set; } 
        public List<string?>? sku { get; set; } 
        public bool is_vintage { get; set; } 
    }

    public class Params    {
        public string? limit { get; set; } 
        public int offset { get; set; } 
        public object? page { get; set; } 
        public object? keywords { get; set; } 
        public string? sort_on { get; set; } 
        public string? sort_order { get; set; } 
        public object? min_price { get; set; } 
        public object? max_price { get; set; } 
        public object? color { get; set; } 
        public int color_accuracy { get; set; } 
        public object? tags { get; set; } 
        public object? taxonomy_id { get; set; } 
        public object? location { get; set; } 
        public object? lat { get; set; } 
        public object? lon { get; set; } 
        public object? region { get; set; } 
        public string? geo_level { get; set; } 
        public string? accepts_gift_cards { get; set; } 
        public string? translate_keywords { get; set; } 
    }

    public class Pagination    {
        public int effective_limit { get; set; } 
        public int effective_offset { get; set; } 
        public int next_offset { get; set; } 
        public int effective_page { get; set; } 
        public int next_page { get; set; } 
    }

}
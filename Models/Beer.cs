using System.Text.Json.Serialization;

namespace Beers.Models
{

    public class Beer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Tagline { get; set; }
        public string? FirstBrewed { get; set; }
        public string? Description { get; set; }

        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }
        public double? Abv { get; set; }
        public double? Ibu { get; set; }
        public double? TargetFg { get; set; }
        public double? TargetOg { get; set; }
        
    }


}

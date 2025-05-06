using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrafficCameras
{
    public class View
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Url")]
        public string Url { get; set; }

        [JsonPropertyName("Status")]
        public string Status { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("VideoUrl")]
        public string VideoUrl { get; set; }
    }

    public class CameraInfo
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Source")]
        public string Source { get; set; }

        [JsonPropertyName("SourceId")]
        public string SourceId { get; set; }

        [JsonPropertyName("Roadway")]
        public string Roadway { get; set; }

        [JsonPropertyName("Direction")]
        public string Direction { get; set; }

        [JsonPropertyName("Latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("Longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("Location")]
        public string Location { get; set; }

        [JsonPropertyName("SortOrder")]
        public int SortOrder { get; set; }

        [JsonPropertyName("Views")]
        public List<View> Views { get; set; }
    }
}

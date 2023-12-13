using Newtonsoft.Json;

namespace ResilienceWithPolly.Models
{
    public class Post
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}

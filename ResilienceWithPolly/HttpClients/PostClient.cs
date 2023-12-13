using Newtonsoft.Json;
using ResilienceWithPolly.Models;

namespace ResilienceWithPolly.HttpClients
{
    public class PostClient : IPostClient
    {
        private readonly HttpClient client;
        public PostClient(HttpClient client)
        {
            this.client = client;
            client.BaseAddress = new Uri("https://my-json-server.typicode.com/typicode/demo/posts");
        }
        public async Task<List<Post>> GetPostsAsync()
        {
            var response = await client.GetAsync($"{client.BaseAddress}").ConfigureAwait(false);
            var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<Post>>(responseData);
        }
    }
}

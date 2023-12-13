using ResilienceWithPolly.Models;

namespace ResilienceWithPolly.HttpClients
{
    public interface IPostClient
    {
        Task<List<Post>> GetPostsAsync();
    }
}

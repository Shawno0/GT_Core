using GT_Core.Domain.Entities;
using TS_Core.Presentation.Services;

namespace GT_Core.Presentation.Services
{
    public class CommentServiceClient : EntityServiceClient<int, Comment>
    {
        public CommentServiceClient(IHttpClientFactory _clientFactory, IConfiguration _config) : base(_clientFactory)
        {
            ServiceUri = $"{_config.GetValue<string>("APIUri")}/comment";
        }
    }
}
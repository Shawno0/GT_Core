using GT_Core.Domain.Entities;
using GT_Core.Presentation.Services;

namespace GT_Core.Presentation.Services
{
    public class CommentServiceClient : EntityServiceClient<int, Comment>
    {
        public CommentServiceClient(IConfiguration _config) : base(_config)
        {
            ServiceUri = $"{_config.GetValue<string>("APIUri")}/comment";
        }
    }
}
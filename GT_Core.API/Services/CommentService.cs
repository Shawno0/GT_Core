using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Models;
using GT_Core.Domain.Entities;

namespace GT_Core.API.Services
{
    public class CommentService : EntityService<int, Comment>
    {
        public CommentService(IApplicationDbContext _dbContext) : base(_dbContext)
        {

        }
    }
}
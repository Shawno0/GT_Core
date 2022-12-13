using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Models;
using GT_Core.Domain.Entities;

namespace GT_Core.API.Services
{
    public class StatusService : EntityService<int, Status>
    {
        public StatusService(IApplicationDbContext _dbContext) : base(_dbContext)
        {

        }
    }
}
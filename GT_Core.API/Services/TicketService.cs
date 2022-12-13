using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Models;
using GT_Core.Domain.Entities;

namespace GT_Core.API.Services
{
    public class TicketService : EntityService<int, Ticket>
    {
        public TicketService(IApplicationDbContext _dbContext) : base(_dbContext)
        {

        }

        public async Task<Result<IEnumerable<Ticket>>> ReadBySeverity(int _severityId, CancellationToken cancellationToken = default)
        {
            IEnumerable<Ticket>? tickets = await Task.Run(() =>
            {
                return DbContext.Tickets.Where(t => t.Severity.Id == _severityId).ToList();
            });

            return new Result<IEnumerable<Ticket>>(true, tickets);
        }

        public async Task<Result<IEnumerable<Ticket>>> ReadByStatus(int _statusId, CancellationToken cancellationToken = default)
        {
            IEnumerable<Ticket>? tickets = await Task.Run(() =>
            {
                return DbContext.Tickets.Where(t => t.Status.Id == _statusId).ToList();
            });

            return new Result<IEnumerable<Ticket>>(true, tickets);
        }

        public async Task<Result<IEnumerable<Ticket>>> ReadByUser(string _userId, CancellationToken cancellationToken = default)
        {
            IEnumerable<Ticket>? tickets = await Task.Run(() =>
            {
                return DbContext.Tickets.Where(t => t.Developer == _userId || t.Consultant == _userId).ToList();
            });

            return new Result<IEnumerable<Ticket>>(true, tickets);
        }
    }
}
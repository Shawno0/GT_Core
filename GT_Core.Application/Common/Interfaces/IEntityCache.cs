using GT_Core.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_Core.Application.Common.Interfaces
{
    public interface IEntityCache<TKey, TEntity> where TEntity : Entity<TKey>
    {
        public int Lifetime { get; }
        public IEnumerable<TEntity> Entities { get; }

        public bool Expired();
        public void Expire();
        public void UpdateEntityCache(IEnumerable<TEntity>? _entities);
    }
}

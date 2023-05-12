using GT_Core.Application.Common.Models;
using GT_Core.Domain.Common;
using GT_Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_Core.Application.Common.Interfaces
{
    public interface IEntityServiceClient<TKey, TEntity> where TEntity : IEntity<TKey>, new()
    {
        Task<Result<TEntity>> Create(TEntity _entity);
        Task<Result<TEntity>> Read(TKey _key);
        Task<Result<IEnumerable<TEntity>>> ReadRange(TKey _firstKey, TKey _lastKey);
        Task<Result<IEnumerable<TEntity>>> ReadAll();
        Task<Result<TEntity>> Update(TEntity _entity);
        Task<Result<TEntity>> Delete(TKey _key);
        Task UpdateEntityCache();
    }
}
using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Models;
using GT_Core.Domain.Common;
using GT_Core.Domain.Entities;
using GT_Core.Domain.Interfaces;

namespace GT_Core.API.Services
{
    public partial class EntityService<TKey, TEntity> : IEntityService<TKey, TEntity> 
        where TEntity : Entity<TKey>, new()
        where TKey : IEquatable<TKey>, IComparable<TKey>
    {
        internal readonly IApplicationDbContext DbContext;

        public EntityService(IApplicationDbContext _dbContext)
        {
            DbContext = _dbContext;
        }

        public async Task<Result<TEntity>> Create(TEntity _entity, CancellationToken _cancellationToken = default)
        {
            var result = await DbContext.Set<TEntity>().AddAsync(_entity, _cancellationToken);

            var changes = await DbContext.SaveChangesAsync(_cancellationToken);

            if (changes > 0)
            {
                return new Result<TEntity>(true, result.Entity);
            }

            return new Result<TEntity>(false, _entity);
        }

        public async Task<Result<TEntity>> Read(TKey _id, CancellationToken _cancellationToken = default)
        {
            TEntity? entity = await Task.Run(() =>
            {
                return DbContext.Set<TEntity>().FirstOrDefault(e => e.Id.Equals(_id));
            });

            if (entity != null)
            {
                return new Result<TEntity>(true, entity);
            }

            return new Result<TEntity>(false, new TEntity());
        }

        public async Task<Result<IEnumerable<TEntity>>> ReadRange(TKey _firstId, TKey _lastId, CancellationToken _cancellationToken = default)
        {
            IEnumerable<TEntity> entitys = await Task.Run(() =>
            {
                return DbContext.Set<TEntity>().Where(e => e.Id.CompareTo(_firstId) >= 0 && e.Id.CompareTo(_lastId) <= 0).ToList();
            });

            if (entitys.Count() > 0)
            {
                return new Result<IEnumerable<TEntity>>(true, entitys);
            }

            return new Result<IEnumerable<TEntity>>(false, new List<TEntity>());
        }

        public async Task<Result<IEnumerable<TEntity>>> ReadAll(CancellationToken _cancellationToken = default)
        {
            IEnumerable<TEntity> entitys = await Task.Run(() =>
            {
                return DbContext.Set<TEntity>().ToList();
            });

            return new Result<IEnumerable<TEntity>>(true, entitys);
        }

        public async Task<Result<TEntity>> Update(TEntity _entity, CancellationToken _cancellationToken = default)
        {
            TEntity? entity = DbContext.Set<TEntity>().FirstOrDefault(e => e.Id.Equals(_entity.Id));

            if (entity != null)
            {
                DbContext.Set<TEntity>().Update(entity);

                entity = _entity;

                var changes = await DbContext.SaveChangesAsync(_cancellationToken);

                if (changes > 0)
                {
                    return new Result<TEntity>(true, entity);
                }
            }

            return new Result<TEntity>(false, _entity);
        }

        public async Task<Result<TEntity>> Delete(TKey _id, CancellationToken _cancellationToken = default)
        {
            TEntity? entity = DbContext.Set<TEntity>().FirstOrDefault(e => e.Id.Equals(_id));

            if (entity != null)
            {
                DbContext.Set<TEntity>().Remove(entity);

                var changes = await DbContext.SaveChangesAsync(_cancellationToken);

                if (changes > 0)
                {
                    return new Result<TEntity>(true, entity);
                }
            }

            return new Result<TEntity>(false, new TEntity());
        }
    }
}
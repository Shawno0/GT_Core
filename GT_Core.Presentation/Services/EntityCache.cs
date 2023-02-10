using GT_Core.Application.Common.Interfaces;
using GT_Core.Domain.Common;

namespace GT_Core.Presentation.Services
{
    public class EntityCache<TKey, TEntity> : IEntityCache<TKey, TEntity> where TEntity : Entity<TKey>
    {
        internal DateTime ExpirationDate { get; set; }
        public int Lifetime { get; internal set; }
        public IEnumerable<TEntity> Entities { get; internal set; }

        public EntityCache(int _lifetime)
        {
            Lifetime = _lifetime;
            ExpirationDate = DateTime.Now.AddMinutes(-1); //Needs to be initialized with an expired date, to ensure it get's update after the first service call.
            Entities = new List<TEntity>();
        }

        public bool Expired()
        {
            return ExpirationDate < DateTime.Now;
        }

        public void Expire()
        {
            ExpirationDate = DateTime.Now.AddMinutes(-1);
        }

        public void UpdateEntityCache(IEnumerable<TEntity>? _entities)
        {
            Entities = _entities ?? new List<TEntity>();
            ExpirationDate = DateTime.Now.AddMinutes(Lifetime);
        }
    }
}

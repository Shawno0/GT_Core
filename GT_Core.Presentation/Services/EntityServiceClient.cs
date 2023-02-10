using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Models;
using GT_Core.Domain.Common;
using GT_Core.Domain.Entities;
using GT_Core.Presentation.Models.ViewModels;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GT_Core.Presentation.Services
{
    public partial class EntityServiceClient<TKey, TEntity> : IEntityServiceClient<TKey, TEntity>
        where TEntity : Entity<TKey>, new()
        where TKey : IEquatable<TKey>, IComparable<TKey>
    {
        internal readonly IHttpClientFactory ClientFactory;
        internal IEntityCache<TKey, TEntity> EntityCache;
        internal string ServiceUri = String.Empty;

        public EntityServiceClient(IHttpClientFactory _clientFactory, IConfiguration _config)
        {
            ClientFactory = _clientFactory;
            EntityCache = new EntityCache<TKey, TEntity>(_config.GetValue<int>("EntityCacheSettings:DefaultLifetimeMinutes"));
        }

        public async Task<Result<TEntity>> Create(TEntity _entity)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{ServiceUri}/create");

            var client = ClientFactory.CreateClient();

            var content = JsonConvert.SerializeObject(_entity);

            StringContent stringContent =
              new StringContent(content, System.Text.Encoding.UTF8,
              "application/json");

            var response = await client.PostAsync(request.RequestUri,
              stringContent);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TEntity? entity = JsonConvert.DeserializeObject<TEntity>(await response.Content.ReadAsStringAsync());

                await UpdateEntityCache();

                return new Result<TEntity>(true, entity);
            }

            return new Result<TEntity>(false, new List<string>() { response.StatusCode.ToString() });
        }

        public async Task<Result<TEntity>> Read(TKey _id)
        {
            if (!EntityCache.Expired())
            {
                return ReadFromCache(_id);
            }

            await UpdateEntityCache();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{ServiceUri}/{_id}/read");

            var client = ClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TEntity? entity = JsonConvert.DeserializeObject<TEntity>(await response.Content.ReadAsStringAsync());

                return new Result<TEntity>(true, entity);
            }

            return new Result<TEntity>(false, new List<string>() { response.StatusCode.ToString() });
        }

        private Result<TEntity> ReadFromCache(TKey _id)
        {
            TEntity entity = EntityCache.Entities.FirstOrDefault(e => e.Id.Equals(_id));

            if (entity == null)
            {
                return new Result<TEntity>(false, new TEntity());
            }

            return new Result<TEntity>(true, entity);
        }

        public async Task<Result<IEnumerable<TEntity>>> ReadRange(TKey _startId, TKey _endId)
        {
            if (!EntityCache.Expired())
            {
                return ReadRangeFromCache(_startId, _endId);
            }

            await UpdateEntityCache();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{ServiceUri}/{_startId}/{_endId}/read");

            var client = ClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                IEnumerable<TEntity>? entity = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(await response.Content.ReadAsStringAsync());

                return new Result<IEnumerable<TEntity>>(true, entity);
            }

            return new Result<IEnumerable<TEntity>>(false, new List<string>() { response.StatusCode.ToString() });
        }

        private Result<IEnumerable<TEntity>> ReadRangeFromCache(TKey _startId, TKey _endId)
        {
            IEnumerable<TEntity> entities = EntityCache.Entities.Where(e => e.Id.CompareTo(_startId) >= 0 && e.Id.CompareTo(_endId) <= 0);

            if (entities.Count() == 0)
            {
                return new Result<IEnumerable<TEntity>>(false, new List<TEntity>());
            }

            return new Result<IEnumerable<TEntity>>(true, entities);
        }

        public async Task<Result<IEnumerable<TEntity>>> ReadAll()
        {
            if (!EntityCache.Expired())
            {
                return ReadAllFromCache();
            }

            var request = new HttpRequestMessage(HttpMethod.Get, $"{ServiceUri}/read");

            var client = ClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                IEnumerable<TEntity>? entity = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(await response.Content.ReadAsStringAsync());

                EntityCache.UpdateEntityCache(entity);

                return new Result<IEnumerable<TEntity>>(true, entity);
            }

            return new Result<IEnumerable<TEntity>>(false, new List<string>() { response.StatusCode.ToString() });
        }

        private Result<IEnumerable<TEntity>> ReadAllFromCache()
        {
            return new Result<IEnumerable<TEntity>>(true, EntityCache.Entities);
        }

        public async Task<Result<TEntity>> Update(TEntity _entity)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"{ServiceUri}/update");

            var client = ClientFactory.CreateClient();

            var content = JsonConvert.SerializeObject(_entity);

            StringContent stringContent =
              new StringContent(content, System.Text.Encoding.UTF8,
              "application/json");

            var response = await client.PutAsync(request.RequestUri,
              stringContent);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TEntity? entity = JsonConvert.DeserializeObject<TEntity>(await response.Content.ReadAsStringAsync());

                await UpdateEntityCache();

                return new Result<TEntity>(true, entity);
            }

            return new Result<TEntity>(false, new List<string>() { response.StatusCode.ToString() });
        }
        public async Task<Result<TEntity>> Delete(TKey _id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{ServiceUri}/{_id}/delete");

            var client = ClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TEntity? entity = JsonConvert.DeserializeObject<TEntity>(await response.Content.ReadAsStringAsync());

                await UpdateEntityCache();

                return new Result<TEntity>(true, entity);
            }

            return new Result<TEntity>(false, new List<string>() { response.StatusCode.ToString() });
        }

        public async Task UpdateEntityCache()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{ServiceUri}/read");

            var client = ClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                IEnumerable<TEntity>? entity = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(await response.Content.ReadAsStringAsync());

                EntityCache.UpdateEntityCache(entity);
            }
        }
    }
}
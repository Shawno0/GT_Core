using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Models;
using GT_Core.Domain.Common;
using GT_Core.Domain.Entities;
using GT_Core.Presentation.Models.ViewModels;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace TS_Core.Presentation.Services
{
    public partial class EntityServiceClient<TKey, TEntity> : IEntityServiceClient<TKey, TEntity>
        where TEntity : Entity<TKey>, new()
        where TKey : IEquatable<TKey>, IComparable<TKey>
    {
        internal readonly IHttpClientFactory ClientFactory;
        internal string ServiceUri = String.Empty;

        public EntityServiceClient(IHttpClientFactory _clientFactory)
        {
            ClientFactory = _clientFactory;
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

                return new Result<TEntity>(true, entity);
            }

            return new Result<TEntity>(false, new List<string>() { response.StatusCode.ToString() });
        }

        public async Task<Result<TEntity>> Read(TKey _id)
        {
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

        public async Task<Result<IEnumerable<TEntity>>> ReadRange(TKey _startId, TKey _endId)
        {
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

        public async Task<Result<IEnumerable<TEntity>>> ReadAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{ServiceUri}/read");

            var client = ClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                IEnumerable<TEntity>? entity = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(await response.Content.ReadAsStringAsync());

                return new Result<IEnumerable<TEntity>>(true, entity);
            }

            return new Result<IEnumerable<TEntity>>(false, new List<string>() { response.StatusCode.ToString() });
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

                return new Result<TEntity>(true, entity);
            }

            return new Result<TEntity>(false, new List<string>() { response.StatusCode.ToString() });
        }
    }
}
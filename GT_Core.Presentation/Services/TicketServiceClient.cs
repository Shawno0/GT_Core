using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Models;
using GT_Core.Domain.Entities;
using GT_Core.Presentation.Models.ViewModels;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GT_Core.Presentation.Services
{
    public class TicketServiceClient : EntityServiceClient<int, Ticket>
    {
        public TicketServiceClient(IConfiguration _config) : base(_config)
        {
            ServiceUri = $"{_config.GetValue<string>("APIUri")}/ticket";
        }

        public async Task<Result<IEnumerable<Ticket>>> ReadByStatus(int _id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{ServiceUri}/status/{_id}/read");

            var response = await Client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                IEnumerable<Ticket>? ticket = JsonConvert.DeserializeObject<IEnumerable<Ticket>>(await response.Content.ReadAsStringAsync());

                return new Result<IEnumerable<Ticket>>(true, ticket);
            }

            return new Result<IEnumerable<Ticket>>(false, new List<string>() { response.StatusCode.ToString() });
        }

        public async Task<Result<IEnumerable<Ticket>>> ReadBySeverity(int _id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{ServiceUri}/severity/{_id}/read");

            var response = await Client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                IEnumerable<Ticket>? ticket = JsonConvert.DeserializeObject<IEnumerable<Ticket>>(await response.Content.ReadAsStringAsync());

                return new Result<IEnumerable<Ticket>>(true, ticket);
            }

            return new Result<IEnumerable<Ticket>>(false, new List<string>() { response.StatusCode.ToString() });
        }

        public async Task<Result<IEnumerable<Ticket>>> ReadByUser(string _id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{ServiceUri}/user/{_id}/read");

            var response = await Client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                IEnumerable<Ticket>? ticket = JsonConvert.DeserializeObject<IEnumerable<Ticket>>(await response.Content.ReadAsStringAsync());

                return new Result<IEnumerable<Ticket>>(true, ticket);
            }

            return new Result<IEnumerable<Ticket>>(false, new List<string>() { response.StatusCode.ToString() });
        }
    }
}
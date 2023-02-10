using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Models;
using GT_Core.Domain.Entities;
using GT_Core.Presentation.Models.ViewModels;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GT_Core.Presentation.Services
{
    public class StatusServiceClient : EntityServiceClient<int, Status>
    {
        public StatusServiceClient(IHttpClientFactory _clientFactory, IConfiguration _config) : base(_clientFactory, _config)
        {
            ServiceUri = $"{_config.GetValue<string>("APIUri")}/status";
        }
    }
}
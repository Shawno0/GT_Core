using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Models;
using GT_Core.Domain.Entities;
using GT_Core.Presentation.Models.ViewModels;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace TS_Core.Presentation.Services
{
    public class SeverityServiceClient : EntityServiceClient<int, Severity>
    {
        public SeverityServiceClient(IHttpClientFactory _clientFactory, IConfiguration _config) : base(_clientFactory)
        {
            ServiceUri = $"{_config.GetValue<string>("APIUri")}/severity";
        }
    }
}
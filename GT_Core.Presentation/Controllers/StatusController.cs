using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Models;
using GT_Core.Domain.Entities;
using GT_Core.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using TS_Core.Presentation.Services;

namespace GT_Core.Presentation.Controllers
{
    public class StatusController : Controller
    {
        StatusServiceClient StatusService;

        public StatusController(StatusServiceClient _statusService)
        {
            StatusService = _statusService;
        }

        public async Task<StatusViewModel> Create(StatusViewModel _status)
        {
            Result<Status> result = await StatusService.Create(_status.Map());

            if (result.Succeeded)
            {
                return new StatusViewModel(result.Entity);
            }

            return _status;
        }

        public async Task<StatusViewModel> Read(int _id)
        {
            Result<Status> result = await StatusService.Read(_id);

            if (result.Succeeded)
            {
                return new StatusViewModel(result.Entity);
            }

            return new StatusViewModel();
        }

        public async Task<IEnumerable<StatusViewModel>> ReadRange(int _startId, int _endId)
        {
            Result<IEnumerable<Status>> result = await StatusService.ReadRange(_startId, _endId);

            if (result.Succeeded)
            {
                return result.Entity?.Select(e => new StatusViewModel(e))?.ToList() ?? new List<StatusViewModel>();
            }

            return new List<StatusViewModel>();
        }

        public async Task<IEnumerable<StatusViewModel>> ReadAll()
        {
            Result<IEnumerable<Status>> result = await StatusService.ReadAll();

            if (result.Succeeded)
            {
                return result.Entity?.Select(e => new StatusViewModel(e))?.ToList() ?? new List<StatusViewModel>();
            }

            return new List<StatusViewModel>();
        }

        public async Task<StatusViewModel> Update(StatusViewModel _status)
        {
            Result<Status> result = await StatusService.Update(_status.Map());

            if (result.Succeeded)
            {
                return new StatusViewModel(result.Entity);
            }

            return _status;
        }

        public async Task<StatusViewModel> Delete(StatusViewModel _status)
        {
            Result<Status> result = await StatusService.Delete(_status.Id);

            if (result.Succeeded)
            {
                return new StatusViewModel(result.Entity);
            }

            return _status;
        }
    }
}
using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Models;
using GT_Core.Domain.Entities;
using GT_Core.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using TS_Core.Presentation.Services;

namespace GT_Core.Presentation.Controllers
{
    public class SeverityController : Controller
    {
        SeverityServiceClient SeverityService;

        public SeverityController(SeverityServiceClient _severityService)
        {
            SeverityService = _severityService;
        }

        public async Task<IActionResult> Index()
        {
            SeverityPanelViewModel model = new SeverityPanelViewModel();

            var severityResult = await SeverityService.ReadAll();

            model.Severities = severityResult.Entity?.Select(s => new SeverityViewModel(s)) ?? new List<SeverityViewModel>();

            return PartialView("_AdminSeverityPanelPartial", model);
        }

        public async Task<IActionResult> Create(SeverityViewModel _severity)
        {
            Result<Severity> result = await SeverityService.Create(_severity.Map());

            return RedirectToAction("Index");
        }

        public async Task<SeverityViewModel> Read(int _id)
        {
            Result<Severity> result = await SeverityService.Read(_id);

            if (result.Succeeded)
            {
                return new SeverityViewModel(result.Entity);
            }

            return new SeverityViewModel();
        }

        public async Task<IEnumerable<SeverityViewModel>> ReadRange(int _startId, int _endId)
        {
            Result<IEnumerable<Severity>> result = await SeverityService.ReadRange(_startId, _endId);

            if (result.Succeeded)
            {
                return result.Entity?.Select(e => new SeverityViewModel(e))?.ToList() ?? new List<SeverityViewModel>();
            }

            return new List<SeverityViewModel>();
        }

        public async Task<IEnumerable<SeverityViewModel>> ReadAll()
        {
            Result<IEnumerable<Severity>> result = await SeverityService.ReadAll();

            if (result.Succeeded)
            {
                return result.Entity?.Select(e => new SeverityViewModel(e))?.ToList() ?? new List<SeverityViewModel>();
            }

            return new List<SeverityViewModel>();
        }

        public async Task<SeverityViewModel> Update(SeverityViewModel _severity)
        {
            Result<Severity> result = await SeverityService.Update(_severity.Map());

            if (result.Succeeded)
            {
                return new SeverityViewModel(result.Entity);
            }

            return _severity;
        }

        public async Task<IActionResult> Delete(SeverityPanelViewModel _severityPanel)
        {
            Result<Severity> result = await SeverityService.Delete(_severityPanel.DeleteSeverity.Id);

            return RedirectToAction("Index");
        }
    }
}
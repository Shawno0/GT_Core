using System.ComponentModel;

namespace GT_Core.Presentation.Models.ViewModels
{
    public class StatusPanelViewModel
    {
        public StatusPanelViewModel()
        {
            NewStatus = new StatusViewModel();
            DeleteStatus = new StatusViewModel();
            Statuses = new List<StatusViewModel>();
        }

        [DisplayName("New Status")]
        public StatusViewModel NewStatus { get; set; }
        [DisplayName("Delete Status")]
        public StatusViewModel DeleteStatus { get; set; }
        public IEnumerable<StatusViewModel> Statuses { get; set; }
    }
}
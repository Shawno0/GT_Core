using System.ComponentModel;

namespace GT_Core.Presentation.Models.ViewModels
{
    public class SeverityPanelViewModel
    {
        public SeverityPanelViewModel()
        {
            NewSeverity = new SeverityViewModel();
            DeleteSeverity = new SeverityViewModel();
            Severities = new List<SeverityViewModel>();
        }

        [DisplayName("New Severity")]
        public SeverityViewModel NewSeverity { get; set; }
        [DisplayName("Delete Severity")]
        public SeverityViewModel DeleteSeverity { get; set; }
        public IEnumerable<SeverityViewModel> Severities { get; set; }
    }
}
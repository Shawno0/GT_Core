namespace GT_Core.Presentation.Models.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<SeverityViewModel> Severities { get; set; }
        public IEnumerable<TicketViewModel> Tickets { get; set; }

        public DashboardViewModel()
        {
            Severities = new List<SeverityViewModel>();
            Tickets = new List<TicketViewModel>();
        }
    }
}
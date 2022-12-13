using Microsoft.AspNetCore.Mvc;

namespace GT_Core.Presentation.Models.ViewModels
{
    public class CreateTicketViewModel
    {
        public CreateTicketViewModel()
        {
            Ticket = new TicketViewModel();
        }

        public IEnumerable<SeverityViewModel> Severities { get; set; }
        public IEnumerable<StatusViewModel> Statuses { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }

        public int Severity { get; set; }
        public int Status { get; set; }
        public string Developer { get; set; }
        public string Consultant { get; set; }
        public TicketViewModel Ticket { get; set; }
    }
}
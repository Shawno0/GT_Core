using GT_Core.Domain.Common;
using GT_Core.Domain.Entities;
using GT_Core.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GT_Core.Presentation.Models.ViewModels
{
    public class TicketViewModel : ArchivableEntity<int>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public SeverityViewModel Severity { get; set; }
        [Required]
        public UserViewModel Developer { get; set; }
        [Required]
        public UserViewModel Consultant { get; set; }

        public TicketViewModel()
        {

        }

        public TicketViewModel(Ticket _ticket, UserManager<ApplicationUser> _userManager)
        {
            Id = _ticket.Id;
            Title = _ticket.Title;
            Description = _ticket.Description;
            Severity = new SeverityViewModel(_ticket.Severity);
            Developer = new UserViewModel(_userManager.Users.FirstOrDefault(u => u.Id == _ticket.Developer));
            Consultant = new UserViewModel(_userManager.Users.FirstOrDefault(u => u.Id == _ticket.Consultant));
            Created = _ticket.Created;
            CreatedBy = _ticket.CreatedBy;
            LastModified = _ticket.LastModified;
            LastModifiedBy = _ticket.LastModifiedBy;
            Archived = _ticket.Archived;
            ArchivedBy = _ticket.ArchivedBy;
        }

        public Ticket Map()
        {
            return new Ticket()
            {
                Id = Id,
                Title = Title,
                Description = Description,
                Created = Created,
                CreatedBy = CreatedBy,
                LastModified = LastModified,
                LastModifiedBy = LastModifiedBy,
                Archived = Archived,
                ArchivedBy = ArchivedBy
            };
        }

        public Ticket Map(int _id)
        {
            return new Ticket()
            {
                Id = _id,
                Title = Title,
                Description = Description,
                Created = Created,
                CreatedBy = CreatedBy,
                LastModified = LastModified,
                LastModifiedBy = LastModifiedBy,
                Archived = Archived,
                ArchivedBy = ArchivedBy
            };
        }
    }
}
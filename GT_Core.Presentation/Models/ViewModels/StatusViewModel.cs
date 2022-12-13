using GT_Core.Domain.Common;
using GT_Core.Domain.Entities;

namespace GT_Core.Presentation.Models.ViewModels
{
    public class StatusViewModel : AuditableEntity<int>
    {
        public string Name { get; set; }

        public StatusViewModel()
        {

        }

        public StatusViewModel(Status _status)
        {
            Id = _status.Id;
            Name = _status.Name;
            Created = _status.Created;
            CreatedBy = _status.CreatedBy;
            LastModified = _status.LastModified;
            LastModifiedBy = _status.LastModifiedBy;
        }

        public Status Map()
        {
            return new Status()
            {
                Id = Id,
                Name = Name,
                Created = Created,
                CreatedBy = CreatedBy,
                LastModified = LastModified,
                LastModifiedBy = LastModifiedBy,
            };
        }

        public Status Map(int _id)
        {
            return new Status()
            {
                Id = _id,
                Name = Name,
                Created = Created,
                CreatedBy = CreatedBy,
                LastModified = LastModified,
                LastModifiedBy = LastModifiedBy,
            };
        }
    }
}
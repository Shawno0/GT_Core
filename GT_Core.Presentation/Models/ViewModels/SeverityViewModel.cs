using GT_Core.Domain.Common;
using GT_Core.Domain.Entities;

namespace GT_Core.Presentation.Models.ViewModels
{
    public class SeverityViewModel : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Icon { get; set; }

        public SeverityViewModel()
        {

        }

        public SeverityViewModel(Severity _severity)
        {
            Id = _severity.Id;
            Name = _severity.Name;
            Icon = _severity.Icon;
            Created = _severity.Created;
            CreatedBy = _severity.CreatedBy;
            LastModified = _severity.LastModified;
            LastModifiedBy = _severity.LastModifiedBy;
        }

        public Severity Map()
        {
            return new Severity()
            {
                Id = Id,
                Name = Name,
                Icon = Icon,
                Created = Created,
                CreatedBy = CreatedBy,
                LastModified = LastModified,
                LastModifiedBy = LastModifiedBy,
            };
        }

        public Severity Map(int _id)
        {
            return new Severity()
            {
                Id = _id,
                Name = Name,
                Icon = Icon,
                Created = Created,
                CreatedBy = CreatedBy,
                LastModified = LastModified,
                LastModifiedBy = LastModifiedBy,
            };
        }
    }
}
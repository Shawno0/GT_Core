using Microsoft.AspNetCore.Identity;

namespace GT_Core.Presentation.Models.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public RoleViewModel()
        {

        }

        public RoleViewModel(IdentityRole _role)
        {
            Id = _role.Id;
            Name = _role.Name;
        }

        public IdentityRole Map()
        {
            return new IdentityRole(Name);
        }
    }
}

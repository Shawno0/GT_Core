using System.ComponentModel;

namespace GT_Core.Presentation.Models.ViewModels
{
    public class RolePanelViewModel
    {
        public RolePanelViewModel()
        {
            NewRole = new RoleViewModel();
            DeleteRole = new RoleViewModel();
            UserRole = new UserRoleViewModel();
            Roles = new List<RoleViewModel>();
            Users = new List<UserViewModel>();
        }

        [DisplayName("New Role")]
        public RoleViewModel NewRole { get; set; }
        [DisplayName("Delete Role")]
        public RoleViewModel DeleteRole { get; set; }
        public UserRoleViewModel UserRole { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}

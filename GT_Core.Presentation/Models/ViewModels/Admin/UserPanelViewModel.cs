using System.ComponentModel;

namespace GT_Core.Presentation.Models.ViewModels
{
    public class UserPanelViewModel
    {
        public UserPanelViewModel()
        {
            NewUser = new UserViewModel();
            DeleteUser = new UserViewModel();
            Users = new List<UserViewModel>();
        }

        [DisplayName("New User")]
        public UserViewModel NewUser { get; set; }
        [DisplayName("Delete User")]
        public UserViewModel DeleteUser { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
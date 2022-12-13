using GT_Core.Infrastructure.Identity;

namespace GT_Core.Presentation.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public UserViewModel()
        {

        }

        public UserViewModel(ApplicationUser _user)
        {
            Id = _user.Id;
            UserName = _user.UserName;
        }
    }
}
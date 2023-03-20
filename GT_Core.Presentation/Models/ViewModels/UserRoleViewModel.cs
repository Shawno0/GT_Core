using Microsoft.AspNetCore.Mvc.Rendering;

namespace GT_Core.Presentation.Models.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        public UserRoleViewModel()
        {

        }

        public UserRoleViewModel(string _userId, string _roleId)
        {
            UserId = _userId;
            RoleId = _roleId;
        }
    }
}

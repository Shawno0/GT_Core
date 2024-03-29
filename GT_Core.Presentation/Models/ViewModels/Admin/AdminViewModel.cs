namespace GT_Core.Presentation.Models.ViewModels
{
    public class AdminViewModel
    {
        public AdminViewModel()
        {
            StatusPanel = new StatusPanelViewModel();
            SeverityPanel = new SeverityPanelViewModel();
            RolePanel = new RolePanelViewModel();
            UserPanel = new UserPanelViewModel();
        }

        public StatusPanelViewModel StatusPanel { get; set; }
        public SeverityPanelViewModel SeverityPanel { get; set; }
        public RolePanelViewModel RolePanel { get; set; }
        public UserPanelViewModel UserPanel { get; set; }
    }
}
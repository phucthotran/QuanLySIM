using System.Web.Mvc;

namespace QuanLySIM.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_Default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Default", action = "Index", id = UrlParameter.Optional },
                new[] { "QuanLySIM.Areas.Admin.Controllers" }
            );
        }
    }
}

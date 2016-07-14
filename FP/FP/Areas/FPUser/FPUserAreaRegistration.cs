using System.Web.Mvc;

namespace FP.Areas.FPUser
{
    public class FPUserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FPUser";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FPUser_default",
                "FPUser/{controller}/{action}/{id}",
                new { area= "FPUser", controller="User", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
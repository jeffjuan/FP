using System.Web.Mvc;

namespace FP.Areas.FPFeature
{
    public class FPFeatureAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FPFeature";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FPFeature_default",
                "FPFeature/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
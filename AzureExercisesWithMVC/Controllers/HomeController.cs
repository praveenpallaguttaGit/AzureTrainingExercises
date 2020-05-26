
using System.Web.Mvc;

namespace AzureExercisesWithMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult AzureWebApp()
        {
            ViewBag.Message = "Deployed Sample Web App in Azure.";
            return View();
        }

    }
}
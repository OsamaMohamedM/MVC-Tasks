using Microsoft.AspNetCore.Mvc;

namespace PipelineGuardian.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
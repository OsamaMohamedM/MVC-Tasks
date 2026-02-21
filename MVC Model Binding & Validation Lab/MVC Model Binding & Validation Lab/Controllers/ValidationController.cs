using Microsoft.AspNetCore.Mvc;

namespace MVC_Model_Binding___Validation_Lab.Controllers
{
    public class ValidationController : Controller
    {
        [AcceptVerbs("GET", "POST")]
        public IActionResult Index(string username)
        {
            var takenNames = new[] { "admin", "osos", "root" };
            if (takenNames.Contains(username.ToLower()))
            {
                return Json("Username is already taken");
            }
            return Json(true);
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace PipelineGuardian.Controllers
{
    public class BankController : Controller
    {
        [Route("balance/{accountId:int:min(1)}")]
        public IActionResult GetBalance(int accountId)
        {
            return Content($"The Current Balance is 5000");
        }

        [Route("bank/user/{username:alpha}")]
        public IActionResult GetUserProfile(string username)
        {
            return Content($"Hello: {username}");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
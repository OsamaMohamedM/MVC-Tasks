using Microsoft.AspNetCore.Mvc;

namespace PipelineGuardian.Areas.Admin.Controllers
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
    }
}
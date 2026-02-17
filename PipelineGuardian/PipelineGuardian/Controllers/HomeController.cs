using BL.Filtters;
using Microsoft.AspNetCore.Mvc;

namespace PipelineGuardian.Controllers
{
    [Route("bank")]
    public class HomeController : Controller
    {
        [FreezeCheck]
        [TransactionLog]
        [HttpGet("withdraw/{accountId:int}")]
        public IActionResult Withdraw(int accountId)
        {
            return Ok($"done {accountId}");
        }
    }
}
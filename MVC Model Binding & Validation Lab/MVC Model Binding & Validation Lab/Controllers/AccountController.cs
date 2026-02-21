namespace MVC_Model_Binding___Validation_Lab.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MVC_Model_Binding___Validation_Lab.Models;

    public class AccountController : Controller
    {
        [BindProperty]
        public EmployeeRegisterViewModel Input { get; set; }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Register")]
        public IActionResult RegisterPost()
        {
            if (Input.Email.Contains("forbidden"))
            {
                ModelState.AddModelError("Input.Email", "Email is blocked");
            }

            if (!ModelState.IsValid)
            {
                return View(Input);
            }

            return RedirectToAction("Success");
        }
    }
}
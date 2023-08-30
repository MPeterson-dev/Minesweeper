using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using Milestone.Services;

namespace Milestone.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessRegister(UserModel user)
        {
            UserDAO securityDAO = new UserDAO();

            if (securityDAO.RegisterUserValid(user))
            {
                return View("RegisterSuccess", user);
            }
            else
            {
                return View("RegisterFailure", user);
            }
        }
    }
}

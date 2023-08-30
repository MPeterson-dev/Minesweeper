using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using Milestone.Services;

namespace Milestone.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(UserModel user)
        {
            UserDAO securityDAO = new UserDAO();

            if (securityDAO.FindUserByNameAndPasswordValid(user))
            {
                return View("LoginSuccess", user);
            }
            else
            {
                return View("LoginFailure", user);
            }
        }
    }
}

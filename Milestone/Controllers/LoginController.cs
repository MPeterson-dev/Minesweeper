using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using Milestone.Services;

namespace Milestone.Controllers
{
    public class LoginController : Controller
    {
        public static int userId = 0;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(UserModel user)
        {
            UserDAO userDAO = new UserDAO();

            if (userDAO.FindUserByNameAndPasswordValid(user))
            {
                userId = userDAO.FindUserIdByNameAndPassword(user);
                return View("LoginSuccess", user);
            }
            else
            {
                return View("LoginFailure", user);
            }
        }
    }
}

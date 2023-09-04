using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using Milestone.Services;

namespace Milestone.Controllers
{
    public class LoginController : Controller
    {
        public static int userId = 0;

        /// <summary>
        /// The function returns a view for the Index page.
        /// </summary>
        /// <returns>
        /// The method is returning a View result.
        /// </returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The ProcessLogin function checks if the user's name and password are valid, and returns a success or failure
        /// view accordingly.
        /// </summary>
        /// <param name="UserModel">The UserModel is a class that represents a user in the system. It typically contains
        /// properties such as username, password, email, and other relevant information about the user.</param>
        /// <returns>
        /// The method is returning an IActionResult.
        /// </returns>
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

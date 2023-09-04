using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using Milestone.Services;

namespace Milestone.Controllers
{
    public class RegisterController : Controller
    {
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
        /// The function processes the registration of a user and returns a view based on whether the registration is
        /// successful or not.
        /// </summary>
        /// <param name="UserModel">The UserModel is a class that represents the data structure of a user. It typically
        /// contains properties such as username, password, email, and other relevant information about the user.</param>
        /// <returns>
        /// The method is returning a IActionResult.
        /// </returns>
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

using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using Milestone.Services;
using Milestone.Utility;
using NLog;

namespace Milestone.Controllers
{
    public class LoginController : Controller
    {
        public static int userId = 0;
        private static Logger logger = LogManager.GetLogger("LoginAppLoggerrule");

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
        /// The ProcessLogin function checks if the provided user credentials are valid, logs the user in if they are, and
        /// returns the appropriate view.
        /// </summary>
        /// <param name="UserModel">UserModel is a class that represents a user in the system. It contains properties such
        /// as UserName and Password, which are used to authenticate the user during the login process.</param>
        /// <returns>
        /// The method is returning an IActionResult. The specific view being returned depends on whether the user login is
        /// successful or not. If the login is successful, the method returns the "LoginSuccess" view with the user model as
        /// the parameter. If the login is unsuccessful, the method returns the "LoginFailure" view with the user model as
        /// the parameter.
        /// </returns>
        [LogActionFilter]
        public IActionResult ProcessLogin(UserModel user)
        {
            UserDAO userDAO = new UserDAO();

            if (userDAO.FindUserByNameAndPasswordValid(user))
            {
                //remember user logged in
                HttpContext.Session.SetString("username", user.UserName);

                userId = userDAO.FindUserIdByNameAndPassword(user);
                MyLogger.GetInstance().Info("Login Success");
                return View("LoginSuccess", user);
            }
            else
            {
                //remove if unsuccessful
                HttpContext.Session.Remove("username");

                MyLogger.GetInstance().Info("Login Failure");
                return View("LoginFailure", user);
            }
        }
    }
}

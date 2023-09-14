using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Milestone.Models;
using Milestone.Utility;

namespace Milestone.Controllers
{
    public class LogActionFilterAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// The function logs the user parameter and a message before leaving the method.
        /// </summary>
        /// <param name="ActionExecutedContext">The ActionExecutedContext is an object that contains information about the
        /// action method that was executed, including the result of the action and any exceptions that occurred during its
        /// execution. It provides access to the controller, the action descriptor, the result, and other related
        /// information.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            UserModel user = (UserModel)((Controller)context.Controller).ViewData.Model;

            MyLogger.GetInstance().Info("Parameter: " + user.ToString());
            MyLogger.GetInstance().Info("Leaving the ProcessLogin method.");
        }

        /// <summary>
        /// The function logs a message when entering the ProcessLogin method.
        /// </summary>
        /// <param name="ActionExecutingContext">The ActionExecutingContext is an object that contains information about the
        /// current action being executed, such as the controller, action method, and route values. It also provides access
        /// to the HttpContext, which contains information about the current HTTP request and response.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            MyLogger.GetInstance().Info("Entering the ProcessLogin method.");
        }
    }
}

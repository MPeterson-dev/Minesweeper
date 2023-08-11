using Milestone.Models;

namespace Milestone.Services
{
    public class SecurityService
    {
        SecuirtyDAO securityDAO = new SecuirtyDAO();

        public bool IsFindUserByNameAndPasswordValid(UserModel user)
        {
            return securityDAO.FindUserByNameAndPassword(user);
        }

        public bool IsRegisterUserValid(UserModel user)
        {
            return securityDAO.RegisterUser(user);
        }
    }
}

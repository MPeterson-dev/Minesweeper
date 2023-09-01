using Milestone.Models;

namespace Milestone.Services
{
    public interface IUserDataService
    {
        bool FindUserByNameAndPasswordValid(UserModel user);

        bool RegisterUserValid(UserModel user);

        int FindUserIdByNameAndPassword(UserModel user);
    }
}

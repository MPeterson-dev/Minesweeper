using Milestone.Models;

namespace Milestone.Services
{
    public interface ISavedGamesDataService
    {

        List<SavedGameModel> AllGames();

        List<SavedGameModel> GetSavedGameByUserId(int id);
        SavedGameModel GetSavedGameByGameId(int id);
        bool DeleteOneGame(int id);
        bool Insert(SavedGameModel savedGame);

    }
}

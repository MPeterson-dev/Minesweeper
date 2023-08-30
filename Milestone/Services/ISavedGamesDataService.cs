namespace Milestone.Services
{
    public interface ISavedGamesDataService
    {

        List<String> AllGames();
        List<String> GetSavedGameById(int id);
        bool Delete(int id);

    }
}

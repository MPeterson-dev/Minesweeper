﻿using Milestone.Models;

namespace Milestone.Services
{
    public interface ISavedGamesDataService
    {

        List<SavedGameModel> AllGames();
        SavedGameModel GetSavedGameByGameId(int id);
        bool DeleteOneGame(int id);

    }
}
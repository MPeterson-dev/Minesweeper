namespace Milestone.Models
{
    public class SavedGameModel
    {

        public int UserId{ get; set; }
        public int GameId{ get; set; }
        public string GameName{ get; set; }
        public string LiveSites { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string ButtonStates { get; set; }
      
        public SavedGameModel(int userId, int gameId, string gameName, string liveSites, string time, string date, string buttonstates)
        {
            UserId = userId;
            GameId = gameId;
            GameName = gameName;
            LiveSites = liveSites;
            Time = time;
            Date = date;
            ButtonStates = buttonstates;
        }

        public SavedGameModel()
        {

        }
    }
}

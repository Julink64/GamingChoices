using System.Collections.Generic;

namespace NewGamingChoices.Models
{
    public class SteamGamesListData
    {
        public SteamGamesListData_Reponse response { get; set; }
    }

    public class SteamGamesListData_Reponse
    {
        public List<Steam_GameInList> games { get; set; }

        public SteamGamesListData_Reponse()
        {
            games = new List<Steam_GameInList>();
        }
    }

    public class Steam_GameInList
    {
        public int appid { get; set; }
    }
}
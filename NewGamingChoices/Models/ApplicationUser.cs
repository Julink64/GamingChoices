using Microsoft.AspNetCore.Identity;
using NewGamingChoices.Data;
using NewGamingChoices.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewGamingChoices.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string SteamId { get; set; }

        //public List<Game> PossessedGames { get; set; }

        public List<GamingMood> GamingMoods { get; set; }

        public string Computer { get; set; }

        public int? AvailableDiscSpace { get; set; }

        /// <summary>
        /// 1 to 5 ; null if the user doesn't have a computer
        /// </summary>
        public int? ComputerPower { get; set; }

        public int InternetNetworkQuality { get; set; }

        /// <summary>
        /// Json string containing possessed consoles
        /// </summary>
        public string Consoles { get; set; }

        [NotMapped]
        public List<GameConsole> ConsolesFullList { get { return JsonSerializer.Deserialize<List<GameConsole>>(Consoles); } }

        public List<ApplicationUser> AskedFriendsList { get; set; }
        public List<ApplicationUser> FriendsList { get; set; }

        public async Task UpdateSteamGamesAsync(ApplicationDbContext dbContext)
        {
            SteamService.GetSteamId(this, dbContext);
            SteamGamesListData steamgameslistdata = await SteamService.GetSteamGamesDataAsync(this.SteamId, dbContext);

            foreach (var game in steamgameslistdata.response.games)
            {
                if (!GamingMoods.Any(gm => gm.Game.SteamAppId == game.appid.ToString())) // Pas de màj si le jeu est déjà dans la bibliothèque locale du joueur
                {

                    Game newgame = await SteamService.GetSteamGameDetailsAsync(game.appid.ToString());
                    if (newgame != null)
                    {
                        GamingMoods.Add(new GamingMood(newgame));
                    }

                    // TODO: Vérifier si le jeu se trouve déjà dans la bibliothèque locale générale
                }
            }

        }

    }

    //public class GCUser
    //{
    //    public string ApplicationUserId { get; set; }
    //    public string Email { get; set; }
    //    public string SteamId { get; set; }

    //    public List<Game> PossessedGames { get; set; }
        
    //    public string Computer { get; set; }

    //    public int? AvailableDiscSpace { get; set; }

    //    public int? ComputerPower { get; set; }

    //    public int InternetNetworkQuality { get; set; }

    //    public List<GameConsole> Consoles { get; set; }

    //    public List<ApplicationUser> AskedFriendsList { get; set; }
    //    public List<ApplicationUser> FriendsList { get; set; }
    //}
}

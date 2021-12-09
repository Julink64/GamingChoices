using Microsoft.AspNetCore.Identity;
using NewGamingChoices.Data;
using NewGamingChoices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewGamingChoices.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string SteamId { get; set; }

        public List<Game> PossessedGames { get; set; }

        public async Task UpdateSteamGamesAsync(ApplicationDbContext dbContext)
        {
            SteamService.GetSteamId(this, dbContext);
            SteamGamesListData steamgameslistdata = await SteamService.GetSteamGamesDataAsync(this.SteamId, dbContext);

            foreach (var game in steamgameslistdata.response.games)
            {
                if (!PossessedGames.Any(g => g.SteamAppId == game.appid.ToString())) // Pas de màj si le jeu est déjà dans la bibliothèque locale du joueur
                {

                    Game newgame = await SteamService.GetSteamGameDetailsAsync(game.appid.ToString());
                    if (newgame != null)
                    {
                        PossessedGames.Add(newgame);
                    }

                    // TODO: Vérifier si le jeu se trouve déjà dans la bibliothèque locale générale
                }
            }

        }

    }
}

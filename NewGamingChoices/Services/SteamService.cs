using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using NewGamingChoices.Models;
using NewGamingChoices.Data;

namespace NewGamingChoices.Services
{
    public static class SteamService
    {

        private static string _APIkey = "EEB952DBEB0A76C1D52A09E47E862A53"; //TODO : A mieux sécuriser

        #region Classic API Call
        public static void GetSteamId(ApplicationUser user, ApplicationDbContext dbContext)
        {
            if (string.IsNullOrEmpty(user.SteamId))
            {
                Regex _accountIdRegex = new Regex(@"^https://steamcommunity\.com/openid/id/(7[0-9]{15,25})$", RegexOptions.Compiled);

                var steamlogin = dbContext.UserLogins.FirstOrDefault(ul => ul.LoginProvider == "Steam" && ul.UserId == user.Id);
                var steamIdstring = steamlogin != null ? steamlogin.ProviderKey : string.Empty;

                var match = _accountIdRegex.Match(steamIdstring);

                if (match.Success)
                {
                    var accountId = match.Groups[1].Value;
                    user.SteamId = accountId;
                }
            }
        }

        public static async System.Threading.Tasks.Task<SteamGamesListData> GetSteamGamesDataAsync(string userSteamId, ApplicationDbContext dbContext)
        {
            if (string.IsNullOrEmpty(userSteamId))
            {
                return new SteamGamesListData(); // Pas de compte Steam
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri("https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key=" + _APIkey + "&steamid=" + userSteamId)
                    };

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseStream = await response.Content.ReadAsStringAsync();
                        var steamgameslistdata = JsonSerializer.Deserialize<SteamGamesListData>(responseStream);

                        return steamgameslistdata;
                    }
                    else
                    {
                        throw new Exception("Erreur de communication avec l'API Steam");
                    }

                }
            }

        }

        private static async System.Threading.Tasks.Task<SteamGameData> _getSteamGameDetailsAsync(string appid)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://store.steampowered.com/api/appdetails/?appids=" + appid)
                };

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStringAsync();

                    JObject obj = JObject.Parse(responseStream);
                    var successtoken = obj.SelectToken(appid).SelectToken("success");

                    if (successtoken.Value<bool>())
                    {
                        var token = obj.SelectToken(appid).SelectToken("data");

                        var steamgamedetails = JsonSerializer.Deserialize<SteamGameData>(token.ToString());

                        return steamgamedetails;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    throw new Exception("Erreur de communication avec l'API Steam");
                }

            }
        }

        private static Game _convertSteamGame(SteamGameData steamgame)
        {
            if (steamgame != null)
            {
                Game convertedgame = new Game();

                convertedgame.Name = steamgame.name;
                convertedgame.Description = steamgame.detailed_description;
                //convertedgame.Genre = 
                convertedgame.IsOnMac = steamgame.platforms.mac;
                convertedgame.MinPlayers = 2;
                //convertedgame.MaxPlayers = TODO
                //convertedgame.MinRequiredPower = steamgame.pc_requirement.minimum TODO
                convertedgame.PlatformPrices.Add(new PlatformPrice { Platform = "PC", Price = steamgame.is_free ? 0 : (steamgame.price_overview != null ? steamgame.price_overview.final / 100 : 0) });
                //convertedgame.Size = steamgame.pc_requirement.minimum TODO
                convertedgame.SteamAppId = steamgame.steam_appid.ToString();
                convertedgame.ThumbnailPath = steamgame.header_image;

                return convertedgame;
            }
            else return null;
            
        }

        public static async System.Threading.Tasks.Task<Game> GetSteamGameDetailsAsync(string appid)
        {
            SteamGameData sgd = await _getSteamGameDetailsAsync(appid);
            return _convertSteamGame(sgd);
        }

        #endregion
    }
}
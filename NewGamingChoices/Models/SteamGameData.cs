using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewGamingChoices.Models
{
    public class SteamGameData
    {
        public int steam_appid { get; set; }
        public string name { get; set; }
        public bool is_free { get; set; }
        public string detailed_description { get; set; }
        public string about_the_game { get; set; }
        public string short_description { get; set; }
        public string supported_languages { get; set; }
        public string header_image { get; set; }
        public Steam_PC_Requirements pc_requirement { get; set; }
        public Steam_Platforms platforms { get; set; }
        public List<Steam_Categories> categories { get; set; }
        public Steam_Price price_overview { get; set; }
    }

    public class Steam_PC_Requirements
    {
        public string minimum { get; set; }
    }

    public class Steam_Platforms
    {
        public bool windows { get; set; }
        public bool mac { get; set; }
        public bool linux { get; set; }
    }

    public class Steam_Categories
    {
        public int id { get; set; }
        public string description { get; set; }
    }

    public class Steam_Price
    {
        public string currency { get; set; }
        public int initial { get; set; }
        public int final { get; set; }
        public int discount_percent { get; set; }
        public string initial_formatted { get; set; }
        public string final_formatted { get; set; }
    }
}
using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text.Json;

namespace DataModels.ScoreBoard
{
    public class ScoreBoard
    {
        [JsonPropertyName("numGames")]
        public object numGames { get; set; }
        [JsonPropertyName("games")]
        public List<Games> games {get; set;}
        

    }
    public class Games
    {//gameId
        [JsonPropertyName("gameId")]
        public object gameId { get; set; }
        [JsonPropertyName("vTeam")]
        public Team vTeam{get; set;}
        [JsonPropertyName("hTeam")]
        public Team hTeam{get; set;}
    }

    public class Team 
    {
        [JsonPropertyName("triCode")]
        public String triCode{get; set;}
        [JsonPropertyName("score")]
        public object score{get; set;}
    }
}
using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text.Json;


namespace DataModels.BoxScore
{
    public class BoxScore
    {   [JsonPropertyName("basicGameData")]
        public BasicGameData basicGameData { get; set; }

        [JsonPropertyName("stats")]
        public Stats stats { get; set; }
        
         
        
    }
    public class Stats
    {
        [JsonPropertyName("vTeam")]
        public Team vTeam{get; set;}
        [JsonPropertyName("hTeam")]
        public Team hTeam{get; set;}

        [JsonPropertyName("activePlayers")]
        public List<ActivePlayers> activePlayers{get; set;}
        
    }
    public class Team 
    {
        [JsonPropertyName("triCode")]
        public String triCode{get; set;}
        [JsonPropertyName("leaders")]
        public Leaders leaders{get; set;}

        [JsonPropertyName("teamId")]
        public String teamId{get; set;}
        public List<ActivePlayers> top{get; set;}
    }


    public class Leaders
    {

        [JsonPropertyName("points")]
        public Stat points{get; set;}

        [JsonPropertyName("rebounds")]
        public Stat rebounds{get; set;}
        
        [JsonPropertyName("assists")]
        public Stat assists{get; set;}
        
    }

    public class Stat
    {
       
        [JsonPropertyName("value")]
        public object value{get; set;}

        [JsonPropertyName("players")]
        public List<Players> players{get; set;}
         
    }
    //players
    public class Players 
    {
        [JsonPropertyName("firstName")]
        public String firstName {get; set;}

        [JsonPropertyName("lastName")]
        public String lastName{get; set;}
    }

    public class ActivePlayers
    {
        [JsonPropertyName("firstName")]
        public String firstName {get; set;}

        [JsonPropertyName("lastName")]
        public String lastName{get; set;}

        [JsonPropertyName("points")]
        public String points{get; set;}

        [JsonPropertyName("teamId")]
        public String teamId{get; set;}
        
        public int getpoints()
        {
            if(Int32.TryParse(this.points, out int j))
            return j;
            
            return 0;
        }
    }

    public class FullTeam
    {
     
        [JsonPropertyName("triCode")]
        public String triCode{get; set;}

        [JsonPropertyName("teamId")]
        public String teamId{get; set;}

        [JsonPropertyName("win")]
        public String win{get; set;}
        [JsonPropertyName("loss")]
        public String loss{get; set;}
        
    

    }
    public class BasicGameData
    {
        [JsonPropertyName("vTeam")]
        public FullTeam vTeam{get; set;}

        [JsonPropertyName("hTeam")]
        public FullTeam hTeam{get; set;}
    }
}
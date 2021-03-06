using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text.Json;

namespace DataModels.BoxScore
{
    public class BoxScore
    {
        //links
        [JsonPropertyName("stats")]
        public Stats stats { get; set; }
    }
    public class Stats
    {
        [JsonPropertyName("vTeam")]
        public Team vTeam{get; set;}
        [JsonPropertyName("hTeam")]
        public Team hTeam{get; set;}
    }
    public class Team 
    {
        [JsonPropertyName("triCode")]
        public String triCode{get; set;}
        [JsonPropertyName("leaders")]
        public Leaders leaders{get; set;}
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
}
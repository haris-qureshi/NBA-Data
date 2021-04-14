using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text.Json;

namespace DataModels.Standings
{
    public class Standings 
    {
        [JsonPropertyName("league")]
        public League league { get; set; }

    }
    public class League 
    {
        //teams
        [JsonPropertyName("standard")]
        public Standard standard { get; set; }
    }
    public class Standard
    {
        //teams
        [JsonPropertyName("teams")]
        public List<Team> teams { get; set; }
    }
    public class Team
    {
        //teamId
        [JsonPropertyName("teamId")]
        public String teamId { get; set; }

        //win
        [JsonPropertyName("win")]
        public String win { get; set; }

        [JsonPropertyName("loss")]
        public String loss { get; set; }
        //confRank
        [JsonPropertyName("confRank")]
        public String confRank { get; set; }

        //teamSitesOnly
        [JsonPropertyName("teamSitesOnly")]
        public TeamSitesOnly teamSitesOnly { get; set; }

    }
    public class TeamSitesOnly
    {
       //teamSitesOnly
        [JsonPropertyName("teamName")]
        public String teamName { get; set; }
        //teamNickname
        [JsonPropertyName("teamNickname")]
        public String teamNickname { get; set; }
    }
}
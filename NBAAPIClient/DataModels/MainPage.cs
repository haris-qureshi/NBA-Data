using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text.Json;

namespace DataModels.MainPage
{
    public class MainPage
    {
        //links
        [JsonPropertyName("links")]
        public Links links { get; set; }
        
        
    }

    public class Links
    {
        [JsonPropertyName("currentDate")]
        public object currentDate { get; set; }
        //todayScoreboard
        [JsonPropertyName("todayScoreboard")]
        public object todayScoreboard { get; set; }

    } 
}
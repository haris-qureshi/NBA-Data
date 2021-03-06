using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text.Json;
using DataModels.ScoreBoard;
using DataModels.MainPage;
using DataModels.BoxScore;
namespace NBAAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            var date = await GetCurrentDate();
            //var ScoreBoard = await GetScoreBoard(date.links.currentDate.ToString());
            var ScoreBoard = await GetScoreBoard("20210304");
            List<BoxScore> boxScores = new List<BoxScore>();
            



            Console.Write("Today's date is:");
            Console.WriteLine(date.links.currentDate);
            Console.WriteLine("This many games were played today:"+ScoreBoard.numGames);
            int i =0;

            foreach (var temp in ScoreBoard.games)
            {
                Console.WriteLine(temp.gameId+" "+temp.hTeam.triCode+": "+temp.hTeam.score+" vs "+temp.vTeam.triCode+": "+temp.vTeam.score);
                boxScores.Add(await GetBoxScore("20210304",temp.gameId.ToString()));
                boxScores[i].stats.hTeam.triCode = temp.hTeam.triCode;
                boxScores[i].stats.vTeam.triCode = temp.vTeam.triCode;
                i++;

                
            }
            Console.WriteLine();
            foreach (var temp in boxScores)
            {
                Console.WriteLine(temp.stats.hTeam.triCode+": ");
                Console.WriteLine("Points "+temp.stats.hTeam.leaders.points.players[0].firstName+" "+temp.stats.hTeam.leaders.points.players[0].lastName+":"+temp.stats.hTeam.leaders.points.value);
                Console.WriteLine("Rebounds "+temp.stats.hTeam.leaders.rebounds.players[0].firstName+" "+temp.stats.hTeam.leaders.rebounds.players[0].lastName+":"+temp.stats.hTeam.leaders.rebounds.value);
                Console.WriteLine("Assists "+temp.stats.hTeam.leaders.assists.players[0].firstName+" "+temp.stats.hTeam.leaders.assists.players[0].lastName+":"+temp.stats.hTeam.leaders.assists.value+"\n");

                Console.WriteLine(temp.stats.vTeam.triCode+": ");
                Console.WriteLine("Points "+temp.stats.vTeam.leaders.points.players[0].firstName+" "+temp.stats.vTeam.leaders.points.players[0].lastName+":"+temp.stats.vTeam.leaders.points.value);
                Console.WriteLine("Rebounds "+temp.stats.vTeam.leaders.rebounds.players[0].firstName+" "+temp.stats.vTeam.leaders.rebounds.players[0].lastName+":"+temp.stats.vTeam.leaders.rebounds.value);
                Console.WriteLine("Assists "+temp.stats.vTeam.leaders.assists.players[0].firstName+" "+temp.stats.vTeam.leaders.assists.players[0].lastName+":"+temp.stats.vTeam.leaders.assists.value+"\n");

            }

        }
        private static async Task<ScoreBoard> GetScoreBoard(string date)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            //var stringTask = client.GetStringAsync("http://data.nba.net/10s/prod/v1/20210305/scoreboard.json");
            string temp = String.Format("http://data.nba.net/10s/prod/v1/{0}/scoreboard.json",date);
            var streamTask = client.GetStreamAsync(temp);


            //var msg = await stringTask;
            ScoreBoard repositories = await JsonSerializer.DeserializeAsync<ScoreBoard>(await streamTask);
            return repositories;
        }
        private static async Task<MainPage> GetCurrentDate()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            var streamTask = client.GetStreamAsync("http://data.nba.net/10s/prod/v1/today.json");
            MainPage mainPage = await JsonSerializer.DeserializeAsync<MainPage>(await streamTask);
            return mainPage;
        }
        private static async Task<BoxScore> GetBoxScore(string Date, string GID)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            string url = String.Format("http://data.nba.net/10s/prod/v1/{0}/{1}_boxscore.json",Date,GID);
            //string url = "http://data.nba.net/10s/prod/v1/20210304/0022000552_boxscore.json";
            var streamTask = client.GetStreamAsync(url);
            
            BoxScore mainPage = await JsonSerializer.DeserializeAsync<BoxScore>(await streamTask);
            return mainPage;
        }
    }
}

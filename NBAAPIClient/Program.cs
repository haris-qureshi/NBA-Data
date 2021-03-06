using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text.Json;
using DataModels.ScoreBoard;
namespace NBAAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            Console.Write("This many games were played today:");
            await GetScoreBoard();
            

        }
        private static async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            var stringTask = client.GetStringAsync("http://data.nba.net/10s/prod/v1/today.json");

            var msg = await stringTask;
            Console.Write(msg);
        }

        private static async Task GetScoreBoard()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            var stringTask = client.GetStringAsync("http://data.nba.net/10s/prod/v1/20210305/scoreboard.json");
            var streamTask = client.GetStreamAsync("http://data.nba.net/10s/prod/v1/20210304/scoreboard.json");


            var msg = await stringTask;
            ScoreBoard repositories = await JsonSerializer.DeserializeAsync<ScoreBoard>(await streamTask);
           Console.WriteLine(repositories.numGames);
           foreach(var game in repositories.games)
           {
               //Console.Write(game.gameId+" ");
               Console.WriteLine(game.vTeam.triCode+" "+game.vTeam.score+" vs "+game.hTeam.triCode+" "+game.hTeam.score);
           }
        }
    }
}

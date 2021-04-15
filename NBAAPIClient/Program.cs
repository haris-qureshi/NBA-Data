using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text.Json;
using DataModels.ScoreBoard;
using DataModels.MainPage;
using DataModels.BoxScore;
using DataModels.Standings;
namespace NBAAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            var date = await GetCurrentDate();
            
            string result = DateTime.Today.AddDays(-1).ToString("yyyyMMdd");
            string tomorrow = DateTime.Today.AddDays(+1).ToString("yyyyMMdd");
            string today = DateTime.Today.ToString("yyyyMMdd");


            //var ScoreBoard = await GetScoreBoard(date.links.currentDate.ToString());
            var ScoreBoard = await GetScoreBoard(result);
            var standings = await GetStandings();
            List<BoxScore> boxScores = new List<BoxScore>();
            
            await Whose_playing(today);
            Console.WriteLine();


            Console.WriteLine("This many games were played "+result+" : "+ScoreBoard.numGames);
            
            int i =0;

            foreach (var temp in ScoreBoard.games)
            {
                boxScores.Add(await GetBoxScore(result,temp.gameId.ToString()));
                Console.WriteLine(temp.hTeam.triCode+": "+temp.hTeam.score+" vs "+temp.vTeam.triCode+": "+temp.vTeam.score);
                boxScores[i].stats.hTeam.triCode = temp.hTeam.triCode;
                boxScores[i].stats.vTeam.triCode = temp.vTeam.triCode;
                i++;
            }


            foreach (var temp in boxScores)
            {   
                Console.WriteLine("\n"+temp.stats.hTeam.triCode+": ");
                //Console.WriteLine("\tPoints: "+temp.stats.hTeam.leaders.points.players[0].firstName+" "+temp.stats.hTeam.leaders.points.players[0].lastName+":"+temp.stats.hTeam.leaders.points.value);
                //Console.WriteLine("\tRebounds: "+temp.stats.hTeam.leaders.rebounds.players[0].firstName+" "+temp.stats.hTeam.leaders.rebounds.players[0].lastName+":"+temp.stats.hTeam.leaders.rebounds.value);
                //Console.WriteLine("\tAssists: "+temp.stats.hTeam.leaders.assists.players[0].firstName+" "+temp.stats.hTeam.leaders.assists.players[0].lastName+":"+temp.stats.hTeam.leaders.assists.value+"\n");
                //Console.WriteLine();
                    
                Boolean flag = true;
                if(flag)
                {
                    GFG gg = new GFG();
                    temp.stats.activePlayers.Sort(gg);
                    flag = false;
                }
                i =0;
                foreach (var point in temp.stats.activePlayers)
                {
                    if(point.teamId.Equals(temp.basicGameData.hTeam.teamId))
                        {
                            if (point.getpoints() >= 10)
                            Console.WriteLine(++i+") "+point.firstName+" "+point.lastName+" : "+point.points); 
                        }
                }
                
                Console.WriteLine("\n"+temp.stats.vTeam.triCode+": ");
                Console.WriteLine("\tPoints: "+temp.stats.vTeam.leaders.points.players[0].firstName+" "+temp.stats.vTeam.leaders.points.players[0].lastName+":"+temp.stats.vTeam.leaders.points.value);
                Console.WriteLine("\tRebounds: "+temp.stats.vTeam.leaders.rebounds.players[0].firstName+" "+temp.stats.vTeam.leaders.rebounds.players[0].lastName+":"+temp.stats.vTeam.leaders.rebounds.value);
                Console.WriteLine("\tAssists: "+temp.stats.vTeam.leaders.assists.players[0].firstName+" "+temp.stats.vTeam.leaders.assists.players[0].lastName+":"+temp.stats.vTeam.leaders.assists.value+"\n");
               
                    
                Console.WriteLine();
                    i =0;
                    foreach (var point in temp.stats.activePlayers)
                    { 
                        if(point.teamId.Equals(temp.basicGameData.vTeam.teamId))
                        {   
                            if (point.getpoints() >= 10)
                            {
                                i++;
                                Console.WriteLine( i+") "+point.firstName+" "+point.lastName+" : "+point.points);
                            }
                        }
                    }
            }
            
            await Whose_playing(tomorrow);
            

           int k=0;
            Console.WriteLine($@"
Standing    Team        win-loss");
            foreach (var team in standings.league.standard.teams)
            {
                string shit= $@"
{++k}) {team.confRank}  {team.teamSitesOnly.teamName} {team.teamSitesOnly.teamNickname}     {team.win} - {team.loss}";
            Console.WriteLine(shit);
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

        private static async Task<Standings> GetStandings()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            string url = String.Format("http://data.nba.net/prod/v1/current/standings_all.json");
            //string url = "http://data.nba.net/10s/prod/v1/20210304/0022000552_boxscore.json";
            var streamTask = client.GetStreamAsync(url);
            
            Standings mainPage = await JsonSerializer.DeserializeAsync<Standings>(await streamTask);
            return mainPage;
        }
        private static async Task Whose_playing(string tomorrow)
        {

            //this is for tomorrows games schedule
            var nextday = await GetScoreBoard(tomorrow);
            List<BoxScore> boxScores = new List<BoxScore>();
            Console.WriteLine("\nThis are games for date "+tomorrow+" : "+nextday.numGames);
            int i =0;
            foreach (var temp in nextday.games)
            {   
                boxScores.Add(await GetBoxScore(tomorrow,temp.gameId.ToString()));
                string shit = $@"{i+1}) {boxScores[i].basicGameData.hTeam.triCode} vs {boxScores[i].basicGameData.vTeam.triCode}
                ";
                Console.WriteLine(shit);
                i++;
            }

            Console.WriteLine();



        }


    }

    class GFG : IComparer<ActivePlayers>
{
    public int Compare(ActivePlayers x, ActivePlayers y)
    {
        if (x == null || y == null)
        {
            return 0;
        }
          
        // "CompareTo()" method
        return y.getpoints().CompareTo(x.getpoints());
          
    }
}
}

using Microsoft.Extensions.Configuration;
using Questao2.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Questao2
{
    public class Program
    {
        public static void Main()
        {
            string teamName = "Paris Saint-Germain";
            int year = 2013;
            int totalGoals = GetTotalScoredGoals(teamName, year);

            Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

            teamName = "Chelsea";
            year = 2014;
            totalGoals = GetTotalScoredGoals(teamName, year);

            Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

            // Output expected:
            // Team Paris Saint - Germain scored 109 goals in 2013
            // Team Chelsea scored 92 goals in 2014
        }

        private static string GetBaseUrl()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();
            string baseUrl = $"{config.GetSection("AppSettings").GetSection("WebApiBaseUrl").Value}";
            return baseUrl;
        }

        public static int GetTotalScoredGoals(string teamName, int year)
        {
            int teams = 2;
            int goals = 0;
            for (int team = 1; team <= teams; team++)
            {
                Results results = RequestData(teamName, team, year, 1);
                if (results.Data != null)
                {
                    goals += GetGoalsSum(results.Data, team);
                    if (results.Total_Pages > 1)
                    {
                        for (int page = 2; page <= results.Total_Pages; page++)
                        {
                            results = RequestData(teamName, team, year, page);
                            if (results.Data != null)
                                goals += GetGoalsSum(results.Data, team);
                        }
                    }
                }

            }
            return goals;
        }

        public static int GetGoalsSum(List<Match> matches, int team)
        {
            int goals = 0;
            if (matches != null)
            {
                if (matches.Count > 0) goals = matches.Sum(match =>
                    team == 1 ? int.Parse(match.Team1Goals) : int.Parse(match.Team2Goals));
            }
            return goals;
        }

        public static Results RequestData(string teamName, int teamNumber, int year, int page)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(GetBaseUrl());
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"?year={year}&team{teamNumber}={teamName}&page={page}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<Results>().Result;
            }
            return new Results();

        }
    }
}
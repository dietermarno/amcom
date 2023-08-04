namespace Questao2.Models
{
    public class Match
    {
        public string? Competition { get; set; }
        public int? Year { get; set; }
        public string? Round { get; set; }
        public string? Team1 { get; set; }
        public string? Team2 { get; set; }
        public string? Team1Goals { get; set; }
        public string? Team2Goals { get; set; }
        public Match()
        {
            Competition = string.Empty;
            Year = 0;
            Round = string.Empty;
            Team1 = string.Empty;
            Team2 = string.Empty;
            Team1Goals = string.Empty;
            Team2Goals = string.Empty;
        }
    }
}

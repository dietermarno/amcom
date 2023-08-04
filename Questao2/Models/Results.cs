namespace Questao2.Models
{
    public class Results
    {
        public int? Page { get; set; }
        public int? Per_Page { get; set; }
        public int? Total { get; set; }
        public int? Total_Pages { get; set; }
        public List<Match>? Data { get; set; }
        public Results()
        {
            Page = 0;
            Per_Page = 0;
            Total = 0;
            Total_Pages = 0;
            Data = new List<Match>();
        }
    }
}

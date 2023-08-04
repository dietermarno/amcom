namespace Questao5.Domain.Entities
{
    public class Movement
    {
        public string? IdMovimento { get; set; }
        public string? IdContaCorrente { get; set; }
        public string? DataMovimento { get; set; }
        public string? TipoMovimento { get; set; }
        public double Valor { get; set; }
        public Movement()
        {
        }
    }
}

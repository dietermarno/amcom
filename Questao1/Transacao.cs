using System;

namespace GereciadorContas
{
    internal class Transacao
    {
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
        public Transacao(string historico, DateTime dataHora, double valorDeposito)
        {
            Data = dataHora;
            Valor = valorDeposito;
            Descricao = historico;
        }
    }
}

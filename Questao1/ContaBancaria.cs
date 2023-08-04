using System;
using System.Collections.Generic;

namespace GereciadorContas
{
    class ContaBancaria
    {
        int NumeroConta { get; set; }
        string NomeTitular { get; set; }
        List<Transacao> Transacoes { get; set; }
        public ContaBancaria(int conta, string titular, double depositoInicial = 0)
        {
            Transacoes = new List<Transacao>();
            if (conta <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(conta), "Informe corretamente o número da conta.");
            }
            NumeroConta = conta;
            titular = titular.Trim();
            if (string.IsNullOrEmpty(titular))
            {
                throw new ArgumentOutOfRangeException(nameof(titular), "Informe corretamente o o nome do titular.");
            }
            NomeTitular = titular;
            if (depositoInicial < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(depositoInicial), "O valor do depósito inicial não pode ser negativo.");
            }
            if (depositoInicial > 0)
                Transacoes.Add(new Transacao("Depósito de abertura", DateTime.Now, depositoInicial));
            else
                Transacoes.Add(new Transacao("Abertura de conta", DateTime.Now, depositoInicial));
        }
        public double Saldo
        {
            get
            {
                double saldo = 0;
                Transacoes.ForEach((transacao) => saldo += transacao.Valor);
                return saldo;
            }
        }
        public void Deposito(double valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(valor), "O valor do depósito precisa ser positivo.");
            }
            Transacoes.Add(new Transacao("Depósito", DateTime.Now, valor));
        }

        public void Saque(double valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(valor), "O valor do saque precisa ser positivo.");
            }
            Transacoes.Add(new Transacao("Saque", DateTime.Now, valor * -1));
        }
        public override string ToString()
        {
            var report = new System.Text.StringBuilder();

            double saldoParcial = 0;
            report.AppendLine("Conta\tTitular");
            report.AppendLine($"{NumeroConta}\t{NomeTitular}");
            report.AppendLine("");
            report.AppendLine("Data\t\tValor\tSaldo\tHistórico");
            foreach (var item in Transacoes)
            {
                saldoParcial += item.Valor;
                report.AppendLine($"{item.Data.ToShortDateString()}\t{item.Valor}\t{saldoParcial}\t{item.Descricao}");
            }

            return report.ToString();
        }
    }
}

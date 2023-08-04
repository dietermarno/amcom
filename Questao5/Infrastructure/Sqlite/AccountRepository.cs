using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Sqlite
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseConfig databaseConfig;
        public AccountRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }
        public AccountBalance GetAccountBalance(string accountId)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);
                connection.Open();
                var account = GetAccount(accountId);
                if (account != null)
                {
                    if (account.Ativo == true)
                    {
                        var query = $"SELECT * FROM movimento WHERE IdContaCorrente = '{accountId}';";
                        var movements = connection.Query<Movement>(query).ToList();
                        double creditValue = movements.Sum(movement => movement.TipoMovimento == "C" ? movement.Valor : 0);
                        double debitValue = movements.Sum(movement => movement.TipoMovimento == "D" ? movement.Valor : 0);
                        return new AccountBalance()
                        {
                            AccountHolder = account.Nome,
                            AccountNumber = account.Numero,
                            Balance = creditValue - debitValue,
                            Date = DateTime.Now
                        };
                    }
                    else
                    {
                        throw new Exception("INACTIVE_ACCOUNT: The account is inactive.");
                    }
                }
                else
                {
                    throw new Exception("INVALID_ACCOUNT: The account not exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsAccountValid(string accountId)
        {
            try
            {
                var account = GetAccount(accountId);
                if (account == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsAccountActive(string accountId)
        {
            try
            {
                var account = GetAccount(accountId);
                if (account == null)
                {
                    return false;
                }
                else
                {
                    if (account.Ativo == null) return false;
                    return (bool)account.Ativo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Account? GetAccount(string accountId)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);
                connection.Open();
                var query = $"SELECT * FROM contacorrente WHERE IdContaCorrente = '{accountId}';";
                var account = connection.Query<Account>(query).FirstOrDefault();
                return account;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

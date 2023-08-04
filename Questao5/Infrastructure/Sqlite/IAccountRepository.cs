using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Sqlite
{
    public interface IAccountRepository
    {
        AccountBalance GetAccountBalance(string accountId);
        bool IsAccountValid(string accountId);
        bool IsAccountActive(string accountId);
        Account? GetAccount(string accountId);
    }
}

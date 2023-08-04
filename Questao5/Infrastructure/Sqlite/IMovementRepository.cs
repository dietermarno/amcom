using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Sqlite
{
    public interface IMovementRepository
    {
        Movement? AddMovement(Movement movement);
        List<Movement> GetMovements(string accountId);
        Movement? GetMovement(string movementId);
        int UpdateMovement(Movement movement);
        int DeleteMovement(string movementId);
    }
}

using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Sqlite
{
    public class MovementRepository : IMovementRepository
    {
        private readonly DatabaseConfig databaseConfig;
        private readonly AccountRepository accountRepository;
        public MovementRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
            accountRepository = new AccountRepository(databaseConfig);
        }
        public Movement? GetMovement(string movementId)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);
                connection.Open();
                var query = $"SELECT * FROM movimento WHERE IdMovimento = '{movementId}';";
                return connection.Query<Movement>(query).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Movement> GetMovements(string accountId)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);
                if (accountRepository.IsAccountValid(accountId))
                {
                    connection.Open();
                    var query = $"SELECT * FROM movimento WHERE IdContaCorrente = '{accountId}';";
                    var movements = connection.Query<Movement>(query).ToList();
                    return movements;
                }
                else
                {
                    throw new Exception("INVALID_ACCOUNT: The account of this movement not exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Movement AddMovement(Movement movement)
        {
            try
            {
                Exception validation = ValidateMovement(movement);
                if (validation != null) throw validation;
                using var connection = new SqliteConnection(databaseConfig.Name);
                connection.Open();
                movement.IdMovimento = Guid.NewGuid().ToString();
                var query = @"INSERT INTO movimento 
                            (IdMovimento, IdContaCorrente, DataMovimento, TipoMovimento, Valor) VALUES 
                            (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor);";
                int count = connection.Execute(query, movement);
                if (count > 0)
                    return movement;
                else
                    throw new Exception("Insert operation into 'movimento' table has not affected any row.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int UpdateMovement(Movement movement)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);
                connection.Open();
                var query = $@"UPDATE movimento SET 
                            DataMovimento = @DataMovimento, TipoMovimento = @TipoMovimento, 
                            Valor = @Valor WHERE IdMovimento = '{movement.IdMovimento}';";

                return connection.Execute(query, movement);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int DeleteMovement(string idMovement)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);
                connection.Open();
                var query = $"DELETE FROM movimento WHERE IdMovimento = '{idMovement}';";
                return connection.Execute(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private Exception? ValidateMovement(Movement movement)
        {
            Exception exception = null;
            if (!accountRepository.IsAccountValid(movement.IdContaCorrente))
            {
                exception = new Exception("INVALID_ACCOUNT: The account of this movement not exists.");
                return exception;
            }
            if (!accountRepository.IsAccountActive(movement.IdContaCorrente))
            {
                exception = new Exception("INACTIVE_ACCOUNT: The account of this movement is inactive.");
                return exception;
            }
            if (movement.Valor <= 0)
            {
                exception = new Exception("INVALID_VALUE: The movement value must be greater than zero.");
                return exception;
            }
            if (movement.TipoMovimento != "C" && movement.TipoMovimento != "D")
            {
                exception = new Exception("INVALID_TYPE: The movement type must be D (Debit) ou C (Credit).");
                return exception;
            }
            return exception;
        }
    }
}

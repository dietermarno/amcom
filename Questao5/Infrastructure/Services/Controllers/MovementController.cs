using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementController : Controller
    {
        private readonly IMovementRepository repository;
        public MovementController(IMovementRepository _context)
        {
            repository = _context;
        }
        [HttpGet("GetAccountMovement/{movementId}")]
        public IActionResult GetAccountMovement(string movementId)
        {
            try
            {
                var movements = repository.GetMovement(movementId);
                if (movements == null)
                {
                    return BadRequest();
                }
                return Ok(movements);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAccountMovements/{accountId}")]
        public IActionResult GetAccountMovements(string accountId)
        {
            try
            {
                var movements = repository.GetMovements(accountId);
                if (movements == null)
                {
                    return BadRequest();
                }
                return Ok(movements.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddAccountMovement")]
        public IActionResult AddAccountMovement(Movement movement)
        {
            try
            {
                movement = repository.AddMovement(movement);
                if (movement == null)
                {
                    return BadRequest();
                }
                return Ok(movement);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateAccountMovement")]
        public IActionResult UpdateAccountMovement(Movement movement)
        {
            try
            {
                int count = repository.UpdateMovement(movement);
                if (count == 0)
                {
                    return BadRequest();
                }
                return Ok(movement);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteAccountMovement/{movementId}")]
        public IActionResult DeleteAccountMovement(string movementId)
        {
            try
            {
                var count = repository.DeleteMovement(movementId);
                if (count == 0)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

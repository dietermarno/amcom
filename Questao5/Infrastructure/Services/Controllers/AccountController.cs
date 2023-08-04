using Microsoft.AspNetCore.Mvc;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository repository;
        public AccountController(IAccountRepository _context)
        {
            repository = _context;
        }
        [HttpGet("GetAccountMovement/{accountId}")]
        public IActionResult GetAccountBallance(string accountId)
        {
            try
            {
                var accountBalance = repository.GetAccountBalance(accountId);
                if (accountBalance == null)
                {
                    return BadRequest();
                }
                return Ok(accountBalance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

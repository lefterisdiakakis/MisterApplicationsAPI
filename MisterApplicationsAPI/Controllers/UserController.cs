using Application.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{

    public class UserController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("sso/authenticate")]
        public async Task<IActionResult> AuthenticateSso(LogInDto loginDto)
        {
            return HandleResult(await Mediator.Send(new AuthenticateSso.Command { LogInDto = loginDto }));
        }

        [AllowAnonymous]
        [HttpGet("sso/token/{code_verifier}")]
        public async Task<IActionResult> GetToken(string code_verifier)
        {
            var res = await Mediator.Send(new Application.User.GetToken.Query { Code_Verifier = code_verifier });

            return Ok(res);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(LogInDto loginDto)
        {
            return HandleResult(await Mediator.Send(new Authenticate.Command { LogInDto = loginDto }));
        }
    }
}

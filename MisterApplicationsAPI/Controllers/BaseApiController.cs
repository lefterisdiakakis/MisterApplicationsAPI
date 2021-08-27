using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Application.Core;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result )
        {
            return result.ResultType switch
            {
                ResultTypes.NotAuthorize => Unauthorized(new { result.ErrorMessage }),
                ResultTypes.Error => BadRequest(new { result.ErrorMessage }),
                ResultTypes.Success => Ok(result.Value),
                _ => Ok(result.Value),
            };
        }
    }
}

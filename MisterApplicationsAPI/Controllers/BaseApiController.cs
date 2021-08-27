using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            switch (result.ResultType)
            {
                case ResultTypes.NotAuthorize:
                    return Unauthorized(new  { result.ErrorMessage });
                case ResultTypes.Error:
                    return BadRequest(new { result.ErrorMessage });
                case ResultTypes.Success:
                    return Ok(result.Value);
                default:
                    return Ok(result.Value);
            }
        }
    }
}

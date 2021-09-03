using Application.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    public class EntityController : BaseApiController
    {        
        [HttpGet("{entity}")]
        public async Task<IActionResult> GetEntity(string entity)
        {
            return HandleResult( await Mediator.Send(new GetAll.Query { Entity=entity}));
        }
    }
}

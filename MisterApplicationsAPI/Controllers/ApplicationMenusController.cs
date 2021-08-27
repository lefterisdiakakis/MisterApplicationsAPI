using Application.ApplicationMenus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{

    public class ApplicationMenusController : BaseApiController
    {
        [Authorize]
        [HttpGet("{AppId}/{Languange}")]
        public async Task<IActionResult> GetMenus(int AppId,string Languange)
        {
            return Ok(await Mediator.Send(new List.Query{ AppId=AppId, Langunage=Languange,UserId=1  }));
        }
    }
}

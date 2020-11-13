using System;
using System.Collections.Generic;
using System.Linq;
using Issue698_4_1_1_Working.Models;
using Microsoft.AspNetCore.Mvc;

namespace Issue698_4_1_1_Working.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("issue698_411_working/v{version:apiVersion}/bars")]
    public class BarController : ControllerBase
    {
        private static readonly IEnumerable<Bar> Bars = Enumerable.Range(0, 50)
            .Select(i => new Bar { Id = Guid.NewGuid() })
            .ToArray();

        [HttpGet("")]
        public IActionResult GetBars() => Ok(Bars);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Issue698_5_0_0_NotWorking.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Issue698_5_0_0_NotWorking.Controllers.V1
{
    [ApiVersion("1")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class FooController : ODataController
    {
        private static readonly IEnumerable<Foo> Foos = Enumerable.Range(0, 50)
            .Select(i => new Foo
            {
                Id = Guid.NewGuid(),
                Bars = Enumerable.Range(0, new Random().Next(4))
                        .Select(i => new Bar
                        {
                            Id = Guid.NewGuid()
                        })
                        .ToArray()
            })
            .ToArray();

        [HttpGet]
        [EnableQuery]
        [ODataRoute("foos")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ODataValue<IEnumerable<Foo>>), Status200OK)]
        public IActionResult GetFoos() => Ok(Foos);
    }
}

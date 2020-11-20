using System;
using System.Collections.Generic;
using System.Linq;
using Issue599.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Issue599.Controllers.V1
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
        public IActionResult GetFoos(ODataQueryOptions<Foo> options)
        {
            try
            {
                options.Validate(new ODataValidationSettings());
            }
            catch (ODataException) { return BadRequest(); }
            catch (Exception) { return BadRequest(); }
            
            // pretend I'm fetching my Foos from some external API
            var apiResult = new
            {
                Data = Foos,
                Count = 581 // this "query" returned the first 50 of 581 Foos
            };

            // suggested approach from: https://github.com/OData/WebApi/issues/2360
            options.ApplyTo(Enumerable.Empty<Foo>().AsQueryable());

            return Ok(new PageResult<Foo>(
                apiResult.Data,
                null,
                apiResult.Count));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Issue702.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Issue702.Controllers.V1
{
    [ApiVersion("1")]
    [ODataRoutePrefix("Foos")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class FoosController : ODataController
    {
        private static readonly Foo[] Foos = Enumerable.Range(0, 50)
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
        [ODataRoute("({id})")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ODataValue<Foo>), Status200OK)]
        public IActionResult GetFoo(Guid id, ODataQueryOptions<Foo> options)
        {
            try
            {
                options.Validate(new ODataValidationSettings());
            }
            catch (ODataException) { return BadRequest(); }
            catch (Exception) { return BadRequest(); }
            
            // pretend I'm fetching my Foos from some external API, just grab a random one
            var apiResult = Foos[new Random().Next(0, Foos.Length)];

            return Ok(new SingleResult<Foo>(new[] { apiResult }.AsQueryable()));
        }

        [HttpGet]
        [ODataRoute]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ODataValue<IEnumerable<Foo>>), Status200OK)]
        public IActionResult Get(ODataQueryOptions<Foo> options)
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

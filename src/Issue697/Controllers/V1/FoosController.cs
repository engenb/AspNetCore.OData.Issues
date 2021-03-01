using System;
using System.Collections;
using System.Collections.Generic;
using Issue697.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Issue697.Controllers.V1
{
    [ApiVersion("1")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class FoosController : ODataController
    {
        [HttpGet]
        [ODataRoute("Foos"), EnableQuery]
        [ProducesResponseType(typeof(ODataValue<IEnumerable<Bar>>), Status200OK)]
        public IActionResult Get() => Ok(Array.Empty<Foo>());

        [HttpGet]
        [ODataRoute("Foos({id})"), EnableQuery]
        [ProducesResponseType(typeof(ODataValue<Foo>), Status200OK)]
        public IActionResult Get(Guid id) => NotFound();

        [HttpPost]
        [ODataRoute("Foos/Bulk"), EnableQuery]
        [ProducesResponseType(typeof(ODataValue<IEnumerable<Foo>>), Status200OK)]
        public IActionResult Bulk([FromBody] BulkIds bulk) => Ok(Array.Empty<Foo>());

        [HttpPost]
        [ODataRoute("Foos/Add"), EnableQuery]
        [ProducesResponseType(typeof(ODataValue<IEnumerable<Bar>>), Status200OK)]
        public IActionResult Add() => NoContent();
    }
}

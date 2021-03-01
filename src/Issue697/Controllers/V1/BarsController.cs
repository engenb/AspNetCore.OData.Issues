using System;
using System.Collections.Generic;
using Issue697.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Issue697.Controllers.V1
{
    [ApiVersion("1")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class BarsController : ODataController
    {
        [HttpGet]
        [ODataRoute("Bars"), EnableQuery]
        [ProducesResponseType(typeof(ODataValue<IEnumerable<Bar>>), Status200OK)]
        public IActionResult Get() => Ok(Array.Empty<Bar>());

        [HttpGet]
        [ODataRoute("Bars({id})"), EnableQuery]
        [ProducesResponseType(typeof(ODataValue<Bar>), Status200OK)]
        public IActionResult Get(Guid id) => NotFound();

        [HttpPost]
        [ODataRoute("Bars/Bulk")]
        [ProducesResponseType(typeof(ODataValue<IEnumerable<Bar>>), Status200OK)]
        public IActionResult Bulk([FromBody] BulkIds bulk) => Ok(Array.Empty<Bar>());

        [HttpPost]
        [ODataRoute("Bars/Add")]
        [ProducesResponseType(typeof(ODataValue<IEnumerable<Bar>>), Status200OK)]
        public IActionResult Add() => NoContent();
    }
}

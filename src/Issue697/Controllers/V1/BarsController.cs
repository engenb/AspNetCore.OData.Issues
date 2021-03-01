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
    public class BarsController : ODataController
    {
        [HttpPost]
        [ODataRoute("Bars/Bulk")]
        [ProducesResponseType(typeof(ODataValue<IEnumerable<Bar>>), Status200OK)]
        public IActionResult Bulk([FromBody] BulkIds bulk, ODataQueryOptions<Bar> options) => NoContent();
    }
}

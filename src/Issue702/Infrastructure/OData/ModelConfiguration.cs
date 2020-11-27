using Issue702.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;

namespace Issue702.Infrastructure.OData
{
    public class ModelConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
        {
            var fooType = builder.EntitySet<Foo>("Foos").EntityType;

            fooType.HasMany(x => x.Bars);

            fooType.HasKey(x => x.Id)
                .Count()
                .Page()
                .Select()
                .Filter()
                .Expand(SelectExpandType.Allowed)
                .OrderBy();

            var barType = builder.EntitySet<Bar>("Bars").EntityType;

            barType.HasOptional(x => x.Foo);

            barType.HasKey(x => x.Id)
                .Count()
                .Page()
                .Select()
                .Filter()
                .Expand(SelectExpandType.Allowed)
                .OrderBy();
        }
    }
}

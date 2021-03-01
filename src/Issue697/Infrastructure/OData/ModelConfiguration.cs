using Issue697.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;

namespace Issue697.Infrastructure.OData
{
    public class ModelConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
        {
            var foo = builder.EntitySet<Foo>("Foos").EntityType;

            foo.HasMany(x => x.Bars);

            var bulkFoosFunc = foo.Collection.Function("Bulk")
                .ReturnsFromEntitySet<Foo>("Foos");
            bulkFoosFunc.Namespace = nameof(Foo);

            foo.HasKey(x => x.Id)
                .Count()
                .Page()
                .Select()
                .Filter()
                .Expand(SelectExpandType.Allowed)
                .OrderBy();

            var bar = builder.EntitySet<Bar>("Bars").EntityType;

            bar.HasOptional(x => x.Foo);

            var bulkBarsFunc = bar.Collection.Function("Bulk")
                .ReturnsFromEntitySet<Bar>("Bars");
            bulkBarsFunc.Namespace = nameof(Bar);

            bar.HasKey(x => x.Id)
                .Count()
                .Page()
                .Select()
                .Filter()
                .Expand(SelectExpandType.Allowed)
                .OrderBy();
        }
    }
}

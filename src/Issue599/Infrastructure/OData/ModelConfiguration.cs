using Issue599.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;

namespace Issue599.Infrastructure.OData
{
    public class ModelConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            var foo = builder.EntitySet<Foo>("foos").EntityType;

            foo.HasMany(x => x.Bars);

            foo.HasKey(x => x.Id)
                .Count()
                .Page()
                .Select()
                .Filter()
                .Expand(SelectExpandType.Allowed)
                .OrderBy();

            var bar = builder.EntitySet<Bar>("bars").EntityType;

            bar.HasOptional(x => x.Foo);

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

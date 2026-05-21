using Microsoft.OpenApi;
using Soenneker.Tests.Unit;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Soenneker.Swashbuckle.SchemaFilters.EnumValues.Tests;

public sealed class EnumValueSchemaFilterTests : UnitTest
{
    [Test]
    public void Default()
    {

    }

    [Test]
    public void Apply_should_ignore_schema_references()
    {
        var filter = new EnumValueSchemaFilter();
        var schema = new OpenApiSchemaReference("RequestDataOptions", new OpenApiDocument(), "3.0");
        var context = new SchemaFilterContext(typeof(string), null!, new SchemaRepository(), null, null);

        filter.Apply(schema, context);
    }
}

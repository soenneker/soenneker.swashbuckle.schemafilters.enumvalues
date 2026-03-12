using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using Microsoft.OpenApi;
using Soenneker.Extensions.Enumerable;
using Soenneker.Reflection.Cache;
using Soenneker.Reflection.Cache.Fields;
using Soenneker.Reflection.Cache.Types;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Soenneker.Swashbuckle.SchemaFilters.EnumValues;

/// <summary>
/// A Swashbuckle Schema filter for EnumValue
/// </summary>
public sealed class EnumValueSchemaFilter : ISchemaFilter
{
    private readonly ReflectionCache _reflectionCache;

    public EnumValueSchemaFilter()
    {
        _reflectionCache = new ReflectionCache();
    }

    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        var mutator = (OpenApiSchema)schema;

        Type? type = context.Type;

        CachedType cachedType = _reflectionCache.GetCachedType(type);

        if (!cachedType.IsEnumValue)
            return;

        CachedField[]? fields = cachedType.GetCachedFields();

        if (fields.IsNullOrEmpty())
            return;

        var openApiValues = new List<JsonNode>();

        foreach (CachedField field in fields)
        {
            if (field.FieldInfo.FieldType.Name != cachedType.Type!.Name)
                continue;

            var enumValue = field.FieldInfo.GetValue(null)?.ToString();

            if (enumValue == null)
                continue;

            openApiValues.Add(JsonValue.Create(enumValue));
        }

        // See https://swagger.io/docs/specification/data-models/enums/
        mutator.Type = JsonSchemaType.String;
        mutator.Enum = openApiValues;
        mutator.Properties = null;
    }
}
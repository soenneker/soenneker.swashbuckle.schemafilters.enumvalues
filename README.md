[![](https://img.shields.io/nuget/v/soenneker.swashbuckle.schemafilters.enumvalues.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.swashbuckle.schemafilters.enumvalues/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.swashbuckle.schemafilters.enumvalues/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.swashbuckle.schemafilters.enumvalues/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.swashbuckle.schemafilters.enumvalues.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.swashbuckle.schemafilters.enumvalues/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.swashbuckle.schemafilters.enumvalues/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.swashbuckle.schemafilters.enumvalues/actions/workflows/codeql.yml)

# Soenneker.Swashbuckle.SchemaFilters.EnumValues

Generates string-enum OpenAPI schemas for types produced by `Soenneker.Gen.EnumValues`.

## Installation

```bash
dotnet add package Soenneker.Swashbuckle.SchemaFilters.EnumValues
```

## Registration

```csharp
using Soenneker.Swashbuckle.SchemaFilters.EnumValues;

builder.Services.AddSwaggerGen(options =>
{
    options.SchemaFilter<EnumValueSchemaFilter>();
});
```

## Example

```csharp
using Soenneker.Gen.EnumValues;

[EnumValue<string>]
public sealed partial class EnvironmentName
{
    public static readonly EnvironmentName Development = new("development");
    public static readonly EnvironmentName Production = new("production");
}
```

The generated OpenAPI schema for `EnvironmentName` becomes:

```yaml
type: string
enum:
  - development
  - production
```

Values are obtained from `ToString()` on static fields whose type exactly matches the enum-value type. The filter changes schema generation only; it does not configure runtime serialization.

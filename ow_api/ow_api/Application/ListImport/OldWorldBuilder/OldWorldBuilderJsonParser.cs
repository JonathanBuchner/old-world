using System.Text.Json;
using NJsonSchema;
using NJsonSchema.Generation;
using NJsonSchema.Validation;
using ow_api.Exceptions;
using ow_api.Models.Api;

namespace ow_api.Application.ListImport.OldWorldBuilder
{
    public class OldWorldBuilderJsonParser
    {
        private readonly JsonSchema _schema;

        public OldWorldBuilderJsonParser()
        {
            var settings = new SystemTextJsonSchemaGeneratorSettings()
            {
                FlattenInheritanceHierarchy = true,
                AlwaysAllowAdditionalObjectProperties = true
            };

            _schema = JsonSchema.FromType<OldWorldBuilderDatasetDto>(settings);
            AllowAdditionalProperties(_schema, []);
        }

        public OldWorldBuilderDatasetDto Parse(JsonElement rawJson)
        {
            var errors = _schema.Validate(rawJson.GetRawText())
                .SelectMany(FlattenValidationError)
                .Where(error => error.Kind != ValidationErrorKind.NoAdditionalPropertiesAllowed)
                .Select(FormatValidationError)
                .Distinct()
                .Select(errorMessage => new ApiMessage() { Message = errorMessage })
                .ToList();

            if (errors.Count > 0)
                throw new OldWorldBuilderJsonDeserializeException(errors);

            try
            {
                var dataset = rawJson.Deserialize<OldWorldBuilderDatasetDto>();

                if (dataset == null)
                    throw new OldWorldBuilderJsonDeserializeException("Old World Builder JSON deserialized to null.");

                return dataset;
            }
            catch (JsonException ex)
            {
                throw new OldWorldBuilderJsonDeserializeException(ex.Message, ex);
            }
        }

        private static IEnumerable<ValidationError> FlattenValidationError(ValidationError error)
        {
            var childErrors = GetChildValidationErrors(error).ToList();

            if (childErrors.Count == 0)
                return [error];

            return childErrors.SelectMany(FlattenValidationError);
        }

        private static IEnumerable<ValidationError> GetChildValidationErrors(ValidationError error)
        {
            if (error is ChildSchemaValidationError childSchemaValidationError)
                return childSchemaValidationError.Errors.SelectMany(errorGroup => errorGroup.Value);

            if (error is MultiTypeValidationError multiTypeValidationError)
                return multiTypeValidationError.Errors.SelectMany(errorGroup => errorGroup.Value);

            return [];
        }

        private static string FormatValidationError(ValidationError error)
        {
            var path = string.IsNullOrWhiteSpace(error.Path) ? "#" : error.Path;
            var property = string.IsNullOrWhiteSpace(error.Property) ? "JSON value" : $"Property '{error.Property}'";

            return $"{property} at {path}: {error.Kind}.";
        }

        private static void AllowAdditionalProperties(JsonSchema schema, HashSet<JsonSchema> visitedSchemas)
        {
            if (!visitedSchemas.Add(schema))
                return;

            schema.AllowAdditionalProperties = true;

            foreach (var property in schema.Properties.Values)
            {
                if (property.Reference != null)
                    AllowAdditionalProperties(property.Reference, visitedSchemas);

                if (property.Item != null)
                    AllowAdditionalProperties(property.Item, visitedSchemas);

                foreach (var oneOfSchema in property.OneOf)
                    AllowAdditionalProperties(oneOfSchema, visitedSchemas);

                foreach (var anyOfSchema in property.AnyOf)
                    AllowAdditionalProperties(anyOfSchema, visitedSchemas);
            }

            if (schema.Item != null)
                AllowAdditionalProperties(schema.Item, visitedSchemas);

            if (schema.AdditionalPropertiesSchema != null)
                AllowAdditionalProperties(schema.AdditionalPropertiesSchema, visitedSchemas);

            foreach (var definition in schema.Definitions.Values)
                AllowAdditionalProperties(definition, visitedSchemas);
        }
    }
}

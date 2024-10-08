using System.Text.Json;
using System.Text.Json.Serialization;
using TariffComparison.WebApi.Infrastructure.Models.Product;

namespace TariffComparison.WebApi.Infrastructure.Models.JsonConverters;

public class BaseProductJsonConverter : JsonConverter<BaseProduct>
{
    public override BaseProduct? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDoc.RootElement;

            if (!rootElement.TryGetProperty("type", out var typeElement))
            {
                throw new JsonException("Missing 'type' property.");
            }

            var type = (ProductType)typeElement.GetInt32();

            BaseProduct? product;

            switch (type)
            {
                case ProductType.BasicElectricityTariff:
                    product = JsonSerializer.Deserialize<BasicElectricityTariffProduct>(rootElement.GetRawText(), options);
                    break;
                case ProductType.PackagedTariff:
                    product = JsonSerializer.Deserialize<PackagedTariffProduct>(rootElement.GetRawText(), options);
                    break;
                default:
                    throw new JsonException($"Unsupported product type: {type}");
            }

            product!.AdditionalKwhCost = product.AdditionalKwhCost / 100;

            return product;
        }
    }

    public override void Write(Utf8JsonWriter writer, BaseProduct value, JsonSerializerOptions options)
    {
        var type = value.Type switch
        {
            ProductType.BasicElectricityTariff => nameof(ProductType.BasicElectricityTariff),
            ProductType.PackagedTariff => nameof(ProductType.PackagedTariff),
            _ => throw new JsonException($"Unsupported product type: {value.Type}")
        };

        var json = JsonSerializer.Serialize(value, value.GetType(), options);

        using (var jsonDoc = JsonDocument.Parse(json))
        {
            writer.WriteStartObject();

            foreach (var property in jsonDoc.RootElement.EnumerateObject())
            {
                property.WriteTo(writer);
            }

            // Adding the "type" discriminator
            writer.WriteString("type", type);

            writer.WriteEndObject();
        }
    }
}

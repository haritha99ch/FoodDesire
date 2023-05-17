namespace FoodDesire.Web.Shared;
public class ShippingAddress {
    [JsonPropertyName("address_line_1")]
    public string No { get; set; }

    [JsonPropertyName("address_line_2")]
    public string Street1 { get; set; }

    [JsonPropertyName("admin_area_1")]
    public string Street2 { get; set; }

    [JsonPropertyName("admin_area_2")]
    public string City { get; set; }

    [JsonPropertyName("postal_code")]
    public string PostalCode { get; set; }

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; }
}

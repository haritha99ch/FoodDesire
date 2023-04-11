using Newtonsoft.Json;

namespace FoodDesire.DAL.Helpers;
internal class ByteArrayConverter : JsonConverter<byte[]> {
    public override byte[] ReadJson(JsonReader reader, Type objectType, byte[]? existingValue, bool hasExistingValue, JsonSerializer serializer) {
        string hexString = (string)reader.Value!;
        return Enumerable.Range(0, hexString.Length / 2).Select(i => Convert.ToByte(hexString.Substring(i * 2, 2), 16)).ToArray();
    }

    public override void WriteJson(JsonWriter writer, byte[]? value, JsonSerializer serializer) {
        writer.WriteValue(BitConverter.ToString(value!).Replace("-", ""));
    }
}

using System.Text;
using Newtonsoft.Json;

namespace Balance.BePaid.Web.Infrastructure.Helpers;

public static class BodyReaderHelper
{
    public static async Task<string> ReadBody(HttpRequest httpRequest, CancellationToken cancellationToken = default)
    {
        try
        {
            using StreamReader reader = new StreamReader(httpRequest.Body, Encoding.UTF8);
            var requestBody = await reader.ReadToEndAsync(cancellationToken);
            return requestBody;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading request body: {ex.Message}");
            throw;
        }
    }

    public static async Task<T?> ReadBody<T>(HttpRequest httpRequest, CancellationToken cancellationToken = default)
    {
        try
        {
            var json = await ReadBody(httpRequest, cancellationToken);
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            throw;
        }
    }
}
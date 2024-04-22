using Balance.BePaid.Domain.Entities;
using Balance.BePaid.Infrastructure.External.BePaid.Configuration;
using Newtonsoft.Json;

namespace Balance.BePaid.Infrastructure.External.BePaid.Helpers;

public static class BePaidConfigurationDeserializationHelper
{
    public static BePaidConfiguration? DeserializeToBePaidConfiguration(PaymentSystemConfiguration configuration)
    {
        var deserializedModel = JsonConvert.DeserializeObject<BePaidConfiguration>(configuration.Data);

        if (deserializedModel is null)
        {
            throw new Exception("error_while_deserialize_bePaid_configuration");
        }

        return deserializedModel;
    }
}
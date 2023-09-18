using System.Text.Json.Serialization;

namespace PayGateMicroService.Domain.Services.PayGateService.Response;

public class PayGateResponse
{
    [JsonPropertyName("PAYGATE_ID")]
    public string PayGateId { get; set; }
    [JsonPropertyName("PAY_REQUEST_ID")]
    public string PayRequestId { get; set; }
    [JsonPropertyName("REFERENCE")]
    public string Reference { get; set; }
    [JsonPropertyName("CHECKSUM")]
    public string Checksum { get; set; }
}
using System.Text.Json.Serialization;

namespace PayGateMicroService.Domain.Services.PayGateService.Request;

public class PayGateRequest
{
    public PayGateRequest( string payGateId, string reference, int amount, string currency, string returnUrl, string transactionDate, string locale, string country, string email, string? notifyUrl = null)
    {
        Reference = reference;
        Amount = amount;
        Currency = currency;
        ReturnUrl = returnUrl;
        TransactionDate = transactionDate;
        Locale = locale;
        Country = country;
        Email = email;
        PayGateId = payGateId;
        NotifyUrl = notifyUrl;
    }
    
    [JsonPropertyName("PAYGATE_ID")]
    public string PayGateId { get; set; }
    [JsonPropertyName("REFERENCE")]
    public string Reference { get; set; }
    [JsonPropertyName("AMOUNT")]
    public int Amount { get; set; }
    [JsonPropertyName("CURRENCY")]
    public string Currency { get; set; }
    [JsonPropertyName("RETURN_URL")]
    public string ReturnUrl { get; set; }
    [JsonPropertyName("TRANSACTION_DATE")]
    public string TransactionDate { get; set; }
    [JsonPropertyName("LOCALE")]
    public string Locale { get; set; }
    [JsonPropertyName("COUNTRY")]
    public string Country { get; set; }
    [JsonPropertyName("EMAIL")]
    public string Email { get; set; }
    [JsonPropertyName("NOTIFY_URL")]
    public string? NotifyUrl { get; set; }
}
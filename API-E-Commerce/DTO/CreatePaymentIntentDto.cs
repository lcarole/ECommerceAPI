namespace API_E_Commerce.DTO;

public class CreatePaymentIntentDto{
    long Amount;
    string Currency;
    Guid OrderId;
}

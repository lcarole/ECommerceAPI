using API_E_Commerce.DTO;
using Stripe;

namespace API_E_Commerce.Services;

public class PaymentService : IPaymentService
{
    StripeClient _stripeClient;

    public PaymentService(StripeClient stripeClient)
    {
        _stripeClient = stripeClient;
    }
    
    public Task<string> CreatePaymentIntent(CreatePaymentIntentDto paymentIntentDto)
    {
        throw new NotImplementedException();
    }
}
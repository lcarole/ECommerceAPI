using API_E_Commerce.DTO;

namespace API_E_Commerce.Services;

public interface IPaymentService
{
    Task<string> CreatePaymentIntent(CreatePaymentIntentDto paymentIntentDto);
}

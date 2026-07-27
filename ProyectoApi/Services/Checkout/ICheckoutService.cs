using ProyectoApi.Dtos;
using ProyectoApi.Dtos.Checkout;
using ProyectoApi.Entities;

namespace ProyectoApi.Services.Checkout
{
    public interface ICheckoutService
    {
        Task<CheckoutSessionResult> CreateCheckoutSessionAsync(CheckoutRequestDto request, string originDomain);
        Task<CheckoutSuccessResult> ProcessCheckoutSuccessAsync(string sessionId);
        Task<IEnumerable<Transaction>> GetTransactionsAsync();
    }
}

using ProyectoApi.Database;
using ProyectoApi.Dtos;
using ProyectoApi.Dtos.Checkout;
using ProyectoApi.Entities;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;

namespace ProyectoApi.Services.Checkout
{
    public class CheckoutService : ICheckoutService
    {
        private readonly MusicStoreDbContext _context;

        public CheckoutService(MusicStoreDbContext context)
        {
            _context = context;
        }

        public async Task<CheckoutSessionResult> CreateCheckoutSessionAsync(CheckoutRequestDto request, string originDomain)
        {
            var songs = await _context.Songs.Where(s => request.SongIds.Contains(s.Id)).ToListAsync();
            
            if (!songs.Any())
            {
                return new CheckoutSessionResult { IsSuccess = false, ErrorMessage = "No se proporcionaron canciones validas." };
            }

            var lineItems = songs.Select(song => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(song.Price * 100), // Stripe expects cents
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = song.Title,
                        Description = $"{song.Artist} - {song.Album}",
                    },
                },
                Quantity = 1,
            }).ToList();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = originDomain + "/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = originDomain + "/cart",
            };

            try 
            {
                var service = new SessionService();
                Session session = await service.CreateAsync(options);

                return new CheckoutSessionResult { IsSuccess = true, Url = session.Url };
            }
            catch (Stripe.StripeException ex)
            {
                // Fallback for invalid API key (for sandbox testing purposes)
                Console.WriteLine("Stripe error: " + ex.Message);
                return new CheckoutSessionResult { IsSuccess = false, ErrorMessage = "La API Key de Stripe es inválida o no está configurada. Por favor, configura una clave de prueba válida de Stripe en appsettings.json." };
            }
        }

        public async Task<CheckoutSuccessResult> ProcessCheckoutSuccessAsync(string sessionId)
        {
            if (string.IsNullOrWhiteSpace(sessionId))
            {
                return new CheckoutSuccessResult { IsSuccess = false, Message = "El parámetro session_id es obligatorio." };
            }

            sessionId = sessionId.Trim();

            try
            {
                var service = new SessionService();
                var session = await service.GetAsync(sessionId);

                if (session.PaymentStatus != "paid")
                {
                    return new CheckoutSuccessResult { IsSuccess = false, Message = "No se completó el pago." };
                }

                var exists = await _context.Transactions.AnyAsync(t => t.SessionId == sessionId);
                if (!exists)
                {
                    var transaction = new Transaction
                    {
                        SessionId = sessionId,
                        Status = "completed",
                        Amount = (decimal)session.AmountTotal / 100m,
                        Date = DateTime.UtcNow
                    };

                    _context.Transactions.Add(transaction);
                    await _context.SaveChangesAsync();
                }

                return new CheckoutSuccessResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new CheckoutSuccessResult { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            return await _context.Transactions.OrderByDescending(t => t.Date).ToListAsync();
        }
    }
}

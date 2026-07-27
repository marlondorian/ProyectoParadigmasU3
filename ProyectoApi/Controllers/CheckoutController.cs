using ProyectoApi.Dtos;
using ProyectoApi.Entities;
using ProyectoApi.Services.Checkout;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost("create-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CheckoutRequestDto request)
        {
            var domain = Request.Headers["Origin"].ToString();
            if (string.IsNullOrEmpty(domain))
            {
                domain = "http://localhost:5173"; // Fallback if origin is not present
            }

            var result = await _checkoutService.CreateCheckoutSessionAsync(request, domain);

            if (!result.IsSuccess)
            {
                return BadRequest(new { error = result.ErrorMessage });
            }

            return Ok(new { url = result.Url });
        }

        [HttpGet("success")]
        public async Task<IActionResult> CheckoutSuccess([FromQuery] string session_id)
        {
            var result = await _checkoutService.ProcessCheckoutSuccessAsync(session_id);

            if (result.IsSuccess)
            {
                return Ok(new { success = true });
            }

            return BadRequest(new { success = false, message = result.Message });
        }
        
        [HttpGet("transactions")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            var transactions = await _checkoutService.GetTransactionsAsync();
            return Ok(transactions);
        }
    }
}

namespace ProyectoApi.Dtos.Checkout
{
    public class CheckoutSessionResult
    {
        public bool IsSuccess { get; set; }
        public string? Url { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class CheckoutSuccessResult
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
}

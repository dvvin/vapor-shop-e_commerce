using API.Errors;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace API.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;
        private readonly string _whSecret;

        public PaymentsController(
            IPaymentService paymentService,
            ILogger<PaymentsController> logger,
            IConfiguration config
        )
        {
            _paymentService = paymentService;
            _logger = logger;
            _whSecret = config.GetSection("StripeSettings")["WhSecret"];
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket == null)
                return BadRequest(new ApiResponse(400, "Problem with your basket"));

            return basket;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _whSecret
            );

            PaymentIntent intent;
            Order order;

            if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
            {
                intent = (PaymentIntent)stripeEvent.Data.Object;
                _logger.LogInformation("Payment failed: {intent.Id}", intent.Id);
                order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                _logger.LogInformation("Order updated to payment failed: {order.Id}", order.Id);
            }
            else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
                intent = (PaymentIntent)stripeEvent.Data.Object;
                _logger.LogInformation("Payment succeeded: {intent.Id}", intent.Id);
                order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                _logger.LogInformation("Order updated to payment received: {order.Id}", order.Id);
            }

            return new EmptyResult();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using QuanLyBanHangAPI.Models;
using QuanLyBanHangAPI.Services.VNPayServices;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IVNPayServices _vNPayServices;
        public VNPayController(IVNPayServices vNPayServices)
        {
            _vNPayServices = vNPayServices;
        }
        [HttpPost]
        public IActionResult CretatePayUrl(PaymentInformationModel model)
        {
            var url = _vNPayServices.CreatePaymentUrl(model, HttpContext);
            return Ok(url);
        }
        [HttpGet("PaymentCallback")]
        public IActionResult PaymentCallback()
        {
            var response = _vNPayServices.PaymentExecute(Request.Query);
            if (response.VnPayResponseCode == "00")
            {
                return Redirect("http://localhost:8080/vnpayreturn" + Request.QueryString);
            }
            return Redirect("http://localhost:8080/vnpayreturn" + Request.QueryString);
        }
    }
}

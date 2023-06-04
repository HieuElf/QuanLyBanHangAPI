using Microsoft.AspNetCore.Http;
using QuanLyBanHangAPI.Models;

namespace QuanLyBanHangAPI.Services.VNPayServices
{
    public interface IVNPayServices
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}

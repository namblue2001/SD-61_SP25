using Microsoft.AspNetCore.Mvc;
using StyleTee.Models;
using System.Net;

namespace StyleTee.Controllers
{
    public class ThanhToanController : Controller
    {
        public IActionResult ThanhToanVNPay(decimal tongTien)
        {
            var tick = DateTime.Now.Ticks.ToString();
            var amount = ((int)tongTien * 100).ToString(); // VNPAY cần x100

            var vnp_Params = new Dictionary<string, string>
    {
        { "vnp_Version", VNPayConfig.vnp_Version },
        { "vnp_Command", VNPayConfig.vnp_Command },
        { "vnp_TmnCode", VNPayConfig.vnp_TmnCode },
        { "vnp_Amount", amount },
        { "vnp_CurrCode", "VND" },
        { "vnp_TxnRef", tick },
        { "vnp_OrderInfo", "Thanh toán đơn hàng #" + tick },
        { "vnp_OrderType", "other" },
        { "vnp_Locale", "vn" },
        { "vnp_ReturnUrl", VNPayConfig.vnp_ReturnUrl },
        { "vnp_IpAddr", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1" },
        { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") }
    };

            var paymentUrl = VNPayHelper.BuildUrl(vnp_Params);
            return Redirect(paymentUrl);
        }
        public IActionResult ReturnVNPay()
        {
            var query = Request.Query;
            var vnp_SecureHash = query["vnp_SecureHash"];
            var inputData = query
                .Where(x => x.Key.StartsWith("vnp_") && x.Key != "vnp_SecureHash")
                .ToDictionary(k => k.Key, v => v.Value.ToString());

            var checkHash = VNPayHelper.HmacSHA512(VNPayConfig.vnp_HashSecret,
                string.Join("&", inputData.OrderBy(x => x.Key).Select(x => $"{x.Key}={WebUtility.UrlEncode(x.Value)}")));

            if (checkHash == vnp_SecureHash)
            {
                var responseCode = query["vnp_ResponseCode"];
                if (responseCode == "00")
                {
                    ViewBag.Message = "Thanh toán thành công!";
                    // Lưu đơn hàng tại đây nếu cần
                }
                else
                {
                    ViewBag.Message = "Thanh toán thất bại: " + responseCode;
                }
            }
            else
            {
                ViewBag.Message = "Sai chữ ký (giả mạo?)";
            }

            return View();
        }
    }
}

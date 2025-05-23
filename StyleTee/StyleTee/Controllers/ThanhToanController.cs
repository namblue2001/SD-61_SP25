using Microsoft.AspNetCore.Mvc;
using StyleTee.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;

namespace StyleTee.Controllers
{
    public class ThanhToanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThanhToanController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult ThanhToanVNPay(decimal tongTien, Guid orderId)
        {
            var amount = ((int)tongTien * 100).ToString(); // VNPAY cần x100

            var vnp_Params = new Dictionary<string, string>
            {
                { "vnp_Version", VNPayConfig.vnp_Version },
                { "vnp_Command", VNPayConfig.vnp_Command },
                { "vnp_TmnCode", VNPayConfig.vnp_TmnCode },
                { "vnp_Amount", amount },
                { "vnp_CurrCode", "VND" },
                { "vnp_TxnRef", orderId.ToString() },
                { "vnp_OrderInfo", "Thanh toán đơn hàng #" + orderId.ToString() },
                { "vnp_OrderType", "other" },
                { "vnp_Locale", "vn" },
                { "vnp_ReturnUrl", VNPayConfig.vnp_ReturnUrl },
                { "vnp_IpAddr", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1" },
                { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") }
            };

            var paymentUrl = VNPayHelper.BuildUrl(vnp_Params);
            return Redirect(paymentUrl);
        }

        public async Task<IActionResult> ReturnVNPay()
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
                    // Lấy mã đơn hàng từ vnp_TxnRef
                    var orderIdStr = query["vnp_TxnRef"].ToString();
                    
                    // Kiểm tra và parse mã đơn hàng
                    if (Guid.TryParse(orderIdStr, out Guid orderId))
                    {
                        // Cập nhật trạng thái thanh toán
                        var donHang = await _context.DonHang.FindAsync(orderId);
                        if (donHang != null)
                        {
                            donHang.trangThaiThanhToan = "Đã thanh toán";
                            await _context.SaveChangesAsync();
                            ViewBag.Message = "Thanh toán thành công!";
                            return RedirectToAction("DatHangThanhCong", "GioHang", new { id = orderId });
                        }
                    }
                    
                    ViewBag.Message = "Không tìm thấy thông tin đơn hàng";
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

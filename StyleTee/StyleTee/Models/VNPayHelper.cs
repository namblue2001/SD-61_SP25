using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace StyleTee.Models
{
    public static class VNPayHelper
    {
        public static string HmacSHA512(string key, string inputData)
        {
            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(inputData));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public static string BuildUrl(Dictionary<string, string> data)
        {
            var sorted = data.OrderBy(x => x.Key);
            var query = string.Join("&", sorted.Select(x => $"{x.Key}={WebUtility.UrlEncode(x.Value)}"));
            string sign = HmacSHA512(VNPayConfig.vnp_HashSecret, query);
            return $"{VNPayConfig.vnp_Url}?{query}&vnp_SecureHash={sign}";
        }
    }
}

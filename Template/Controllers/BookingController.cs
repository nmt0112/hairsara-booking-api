using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Storage;
using Storage.Models;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace Template.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="User")]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;

        public BookingController(IHttpClientFactory httpClientFactory, ApplicationDbContext context, UserManager<AspNetUsers> userManager)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            string accessToken = Request.Cookies["token"];
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            if (string.IsNullOrEmpty(accessToken))
            {
                // Người dùng chưa đăng nhập, có thể chuyển hướng hoặc trả về trang đăng nhập
                return RedirectToAction("Login", "Home");
            }
            var userId = _userManager.GetUserId(User);
            var customerId = _context.Customer
                .Where(c => c.IdUserCustomer == userId)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            // Đặt giá trị vào ViewBag
            ViewBag.IdCustomer = customerId;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetViTris()
        {
            using (HttpClient client = new HttpClient())
            {
                var apiEndpoint = "https://localhost:7271/ViTri/ListViTri";
                var response = await client.GetAsync(apiEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var vitris = JsonConvert.DeserializeObject<List<ViTri>>(responseBody);

                    return Json(vitris);
                }
                else
                {
                    return Json(new { error = "Failed to get ViTri" });
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetChiNhanhsByViTri(int idViTri)
        {
            using (HttpClient client = new HttpClient())
            {
                var apiEndpoint = $"https://localhost:7271/ChiNhanh/GetByViTri?idViTri={idViTri}";
                var response = await client.GetAsync(apiEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var chinhanhs = JsonConvert.DeserializeObject<List<ChiNhanh>>(responseBody);

                    return Json(chinhanhs);
                }
                else
                {
                    return Json(new { error = "Failed to get ChiNhanh" });
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetDanhMucs()
        {
            using (HttpClient client = new HttpClient())
            {
                var apiEndpoint = "https://localhost:7271/DanhMuc/ListDanhMuc";
                var response = await client.GetAsync(apiEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var danhmucs = JsonConvert.DeserializeObject<List<DanhMuc>>(responseBody);

                    return Json(danhmucs);
                }
                else
                {
                    return Json(new { error = "Failed to get DanhMuc" });
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetDichVusByDanhMuc(int idDanhMuc)
        {
            using (HttpClient client = new HttpClient())
            {
                var apiEndpoint = $"https://localhost:7271/DichVu/GetByDanhMuc?idDanhMuc={idDanhMuc}";
                var response = await client.GetAsync(apiEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var dichvus = JsonConvert.DeserializeObject<List<DichVu>>(responseBody);

                    return Json(dichvus);
                }
                else
                {
                    return Json(new { error = "Failed to get Dịch Vụ" });
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetBarbersByChiNhanh(int IdChiNhanhWork)
        {
            using (HttpClient client = new HttpClient())
            {
                var apiEndpoint = $"https://localhost:7271/Barber/BarberByChiNhanh?chiNhanhId={IdChiNhanhWork}";
                var response = await client.GetAsync(apiEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var barbers = JsonConvert.DeserializeObject<List<Barber>>(responseBody);

                    return Json(barbers);
                }
                else
                {
                    return Json(new { error = "Failed to get Baber" });
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerId()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var customerId = await _context.Customer
                    .Where(c => c.IdUserCustomer == userId)
                    .Select(c => c.Id)
                    .FirstOrDefaultAsync();

                return Json(new { customerId });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }
}

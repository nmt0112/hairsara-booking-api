using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Storage.Models.Authentication.Login;
using Storage.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using Template.Models;
using Microsoft.AspNetCore.Identity;
using Storage;
using Microsoft.AspNetCore.Identity;

using PagedList;
using Storage.Models.Authentication.SignUp;
using Microsoft.EntityFrameworkCore;

namespace Template.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, ApplicationDbContext context, UserManager<AspNetUsers> userManager)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _context = context;
            _userManager = userManager;
        }
        private bool IsTokenExpired(string token)
        {
            // Lấy thời gian hết hạn của token
            DateTime expirationDate = DateTime.UtcNow + TimeSpan.FromHours(1);

            // So sánh thời gian hiện tại với thời gian hết hạn của token
            return expirationDate < DateTime.UtcNow;
        }
        public IActionResult Index()
        {
            string token = Request.Cookies["token"];
            if (IsTokenExpired(token))
            {
                return Logout();
            }
            ViewBag.Token = token;

            HomeModels obj = new HomeModels
            {
                ListDichVu = _context.DichVu.ToList(),
                ListChiNhanh = _context.ChiNhanh.ToList()
            };
            return View(obj);
        }
        public async Task<IActionResult> Booking()
        {
            var userId = _userManager.GetUserId(User);
            var customerId = await _context.Customer
                .Where(c => c.IdUserCustomer == userId)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (customerId != 0)
            {
                // Gọi API để lấy danh sách lịch hẹn của khách hàng
                var appointments = _context.Booking
                    .Include(a => a.Barber)
                    .Include(a => a.DichVu)
                    .Include(a => a.ChiNhanh)
                    .Include(a => a.Customer)
                    .Where(a => a.IdCustomer == customerId)
                    .ToList();

                // Truyền danh sách lịch hẹn đến View
                return View(appointments);
            }
            else
            {
                // Xử lý khi không tìm thấy customerId
                return View("Error");
            }
        }


        private async Task<List<Booking>> GetBookingsByCustomer(int customerId)
        {
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                var apiUrl = $"https://localhost:7271/Booking/GetByCustomer?idCustomer={customerId}";
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var bookings = JsonConvert.DeserializeObject<List<Booking>>(responseBody);

                    return bookings;
                }
                else
                {
                    // Xử lý khi gọi API không thành công
                    return new List<Booking>();
                }
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser registerModel)
        {
            using (HttpClient client = new HttpClient())
            {
                var apiUrl = "https://localhost:7271/Account/Authentication/Register";
                var jsonContent = JsonConvert.SerializeObject(registerModel);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Xử lý logic khi đăng ký thành công (ví dụ: đăng nhập người dùng, chuyển hướng, v.v.)
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    // Xử lý logic khi đăng ký thất bại (ví dụ: hiển thị thông báo lỗi)
                    return View();
                }
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            using (HttpClient client = new HttpClient())
            {
                var ApiUrl = "https://localhost:7271/Account/Authentication/Login";
                var jsonContent = JsonConvert.SerializeObject(loginModel);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(ApiUrl, content);
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ApiTokenResponse>(responseBody);
                string token = responseData.Token;

                if (response.IsSuccessStatusCode)
                {
                    DateTime expiration = responseData.Expiration;
                    HttpContext.Response.Cookies.Append("token", token, new CookieOptions
                    {
                        Expires = responseData.Expiration,
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None
                    });
                    var cookieOptions = new CookieOptions
                    {
                        Secure = true,
                        HttpOnly = false,
                        SameSite = SameSiteMode.Strict,
                        Expires = responseData.Expiration
                    };
                    // Giải mã token để truy cập thông tin claim
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(responseData.Token) as JwtSecurityToken;
                    // Lấy giá trị của các claim
                    string userName = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
                    Response.Cookies.Append("userName", userName, cookieOptions);
                    string userId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                    Response.Cookies.Append("userId", userId, cookieOptions);
                    //var customer = _context.Customer.SingleOrDefault(c => c.IdUserCustomer == userId);
                    //if (customer != null) {
                    //    Response.Cookies.Append("idCustomer", customer.Id.ToString(), cookieOptions);
                    //}                  
                    string role = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
                    Response.Cookies.Append("role", role, cookieOptions);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            Response.Cookies.Delete("userName");
            Response.Cookies.Delete("idCustomer");
            Response.Cookies.Delete("userId");
            Response.Cookies.Delete("role");
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> LoadChiNhanh()
        {
            using (HttpClient client = new HttpClient())
            {
                var apiEndpoint = "https://localhost:7271/ChiNhanh/ListChiNhanh";
                var response = await client.GetAsync(apiEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var chiNhanhList = JsonConvert.DeserializeObject<List<ChiNhanh>>(responseBody);

                    return PartialView("_ChiNhanhPartial", chiNhanhList);
                }
                else
                {
                    // Xử lý khi gọi API không thành công
                    return PartialView("_ErrorPartial");
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> LoadDichVu()
        {
            using (HttpClient client = new HttpClient())
            {
                 var apiEndpoint = "https://localhost:7271/DichVu/ListDichVu";
                var response = await client.GetAsync(apiEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var dichVuList = JsonConvert.DeserializeObject<List<DichVu>>(responseBody);

                    return PartialView("_DichVuPartial", dichVuList);
                }
                else
                {
                    // Xử lý khi gọi API không thành công
                    return PartialView("_ErrorPartial");
                }
            }
        }
        
        public ActionResult Services(int? page)
        {
            var dichvus = _context.DichVu.ToList();
            int pageNumber = page ?? 1; // Số trang hiện tại, mặc định là 1
            int pageSize = 3; // Số mục trên mỗi trang
                              // Sử dụng PagedList để phân trang danh sách styles
            IPagedList<DichVu> pagedservices = dichvus.ToPagedList(pageNumber, pageSize);

            // Tạo danh sách số trang
            var pageNumbers = Enumerable.Range(1, pagedservices.PageCount);

            // Truyền danh sách số trang vào ViewBag
            ViewBag.PageNumbers = pageNumbers;

            return View(pagedservices);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
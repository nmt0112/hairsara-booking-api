
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage;

namespace Template.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class HomeAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeAdminController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeAdminController(ILogger<HomeAdminController> logger, IHttpClientFactory httpClientFactory, ApplicationDbContext context)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _context = context;
        }
        // GET: Admin/Home
        public ActionResult Index()
        {

            var numberOfUsers = _context.Users.Count();
            ViewBag.NumberOfUsers = numberOfUsers;
            
            // Truyền số lượng User vào ViewBag
            var numberOfCustomer = _context.Customer.Count();
            ViewBag.NumberOfCustomer = numberOfCustomer;

            // Truyền số lượng barbers vào ViewBag
            var numberOfBarbers = _context.Barber.Count();
            ViewBag.NumberOfBarbers = numberOfBarbers;

            // Truyền số lượng Services vào ViewBag
            var numberOfServices = _context.DichVu.Count();
            ViewBag.NumberOfServices = numberOfServices;

            var numberOfDanhMucs = _context.DanhMuc.Count();
            ViewBag.NumberOfDanhMucs = numberOfDanhMucs;
            
            var numberOfViTris = _context.ViTri.Count();
            ViewBag.NumberOfViTris = numberOfViTris;
            
            var numberOfChiNhanhs = _context.ChiNhanh.Count();
            ViewBag.NumberOfChiNhanhs = numberOfChiNhanhs;

            // Truyền số lượng Rating vào ViewBag
            var numberOfAppointments = _context.Booking.Count();
            ViewBag.NumberOfAppointments = numberOfAppointments;

            return View();
        }
    }
}

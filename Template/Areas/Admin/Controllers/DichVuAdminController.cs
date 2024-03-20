using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage;

namespace Template.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DichVuAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeAdminController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public DichVuAdminController(ILogger<HomeAdminController> logger, IHttpClientFactory httpClientFactory, ApplicationDbContext context)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _context = context;
        }
        public IActionResult Index()
        {
            var dichvus = _context.DichVu.Include(a => a.DanhMuc).ToList();
            ViewBag.DanhMucList = _context.DanhMuc.ToList();
            return View(dichvus);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage;

namespace Template.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ChiNhanhAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeAdminController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public ChiNhanhAdminController(ILogger<HomeAdminController> logger, IHttpClientFactory httpClientFactory, ApplicationDbContext context)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _context = context;
        }
        public IActionResult Index()
        {
            var chinhanhs = _context.ChiNhanh.Include(a => a.ViTri).ToList();
            ViewBag.ViTriList = _context.ViTri.ToList();
            return View(chinhanhs);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage;

namespace Template.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DanhMucAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeAdminController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public DanhMucAdminController(ILogger<HomeAdminController> logger, IHttpClientFactory httpClientFactory, ApplicationDbContext context)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.DanhMuc.ToList());
        }
    }
}

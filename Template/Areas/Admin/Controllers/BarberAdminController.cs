using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Storage;
using Storage.Models.Authentication.SignUp;
using System.Net.Http.Headers;
using System.Text;

namespace Template.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BarberAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarberAdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var barbers = _context.Barber.Include(a => a.ChiNhanh).ToList();
            ViewBag.ChiNhanhList = _context.ChiNhanh.ToList();
            return View(barbers);
        }     
    }
}

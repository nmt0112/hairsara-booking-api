using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage;

namespace Template.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BookingAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingAdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var appointments = _context.Booking.Include(a => a.Barber).Include(a => a.DichVu).Include(a=>a.ChiNhanh).ToList();

            // Lặp qua danh sách appointments để gán NameCustomer tương ứng với IdCustomer
            foreach (var appointment in appointments)
            {
                var customerId = appointment.IdCustomer;
                var customer = _context.Customer.FirstOrDefault(c => c.Id == customerId);
                var nameCustomer = customer != null ? customer.NameCustomer : string.Empty;
                appointment.NameCustomer = nameCustomer;
            }

            return View(appointments);
        }
    }
}

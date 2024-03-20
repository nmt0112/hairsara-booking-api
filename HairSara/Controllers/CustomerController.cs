using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage.Services.ServiceBooking;
using Storage;
using HairSara.Models;

namespace HairSara.Controllers
{
    public class CustomerController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        public CustomerController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [HttpGet("GetIdCustomer")]
        public async Task<IActionResult> GetCustomerId()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var customer = await _context.Customer
                    .Where(c => c.IdUserCustomer == userId)
                    .FirstOrDefaultAsync();

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Message = "Thất Bại", IsSucccess = false });
            }
        }
    }
}

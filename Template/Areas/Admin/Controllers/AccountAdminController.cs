using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage;
using Storage.Models;

namespace Template.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AccountAdminController : Controller
    {
        private ApplicationDbContext _context;

        public AccountAdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            var usersWithRoles = _context.Users
                .Select(u => new UserWithRoles
                {
                    User = u,
                    Roles = _context.UserRoles.Where(ur => ur.UserId == u.Id)
                        .Select(ur => _context.Roles.FirstOrDefault(r => r.Id == ur.RoleId).Name)
                        .ToList()
                })
                .ToList();

            return View(usersWithRoles);
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Storage.Models.Authentication.Login;
using Storage.Models.Authentication.SignUp;
using Storage.Models;
using Storage.Services.ServiceUser;
using Storage;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Storage.Services.ServiceEmail;
using HairSara.Models;

namespace HairSara.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("Account/[controller]")]
    [ApiController]
    public class BarberAdminController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IUser _userService;
        private readonly ApplicationDbContext _context;
        public BarberAdminController(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, IEmailService emailService, IConfiguration configuration, IUser userService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _configuration = configuration;
            _userService = userService;
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser, int chiNhanhId)
        {
            var tokenRole = await _userService.CreateUserWithTokenAsync(registerUser);

            if (tokenRole.IsSuccess)
            {
                // Gán quyền "Barber" cho người dùng
                await _userService.AssignRoleToUserAsync(new List<string> { "Barber" }, tokenRole.Response.User);

                // Create a new Barber record
                var barber = new Barber()
                {
                    NameBarber = registerUser.Username,
                    IdUserBarber = tokenRole.Response.User.Id,
                    IdChiNhanhWork = chiNhanhId
                };

                // Add the Barber record to the database
                await _context.Barber.AddAsync(barber);
                await _context.SaveChangesAsync();

                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { tokenRole.Response.Token, email = registerUser.Email }, Request.Scheme);
                var message = new Message(new String[] { registerUser.Email }, "Confirmation email link", confirmationLink!);
                _emailService.SendEmail(message);

                return StatusCode(StatusCodes.Status200OK,
                            new Response { Status = "Success", Message = "Successfully registered as Barber" });
            }

            return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Message = tokenRole.Message, IsSucccess = false });
        }


        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = "Email Verified SuccessFully" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Error", Message = "This User Doesnot exist" });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}

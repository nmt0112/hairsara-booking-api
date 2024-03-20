
using HairSara.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Storage;
using Storage.Models;
using Storage.Models.Authentication.Login;
using Storage.Models.Authentication.SignUp;
using Storage.Services.ServiceEmail;
using Storage.Services.ServiceUser;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HairSara.Controllers
{
    [Route("Account/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IUser _userService;
        private readonly ApplicationDbContext _context;
        public AuthenticationController(UserManager<IdentityUser> userManager,
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
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            var tokenrole = await _userService.CreateUserWithTokenAsync(registerUser);

            if (tokenrole.IsSuccess)
            {
                // Gán quyền "User" cho người dùng
                await _userService.AssignRoleToUserAsync(new List<string> { "User" }, tokenrole.Response.User);

                // Create a new Customer record
                var customer = new Customer()
                {
                    NameCustomer = registerUser.Username,
                    IdUserCustomer = tokenrole.Response.User.Id
                };

                // Add the Customer record to the database
                await _context.Customer.AddAsync(customer);
                await _context.SaveChangesAsync();

                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { tokenrole.Response.Token, email = registerUser.Email }, Request.Scheme);

                // Use HTML for the email content
                var htmlContent = $@"
                    <p>Gửi {registerUser.Username},</p>
                    <p>Cảm ơn bạn đã đăng ký. Hãy click vào ô xác nhận để xác nhận tài khoản của bạn</p>
                    <p><a href='{confirmationLink}' style='display: inline-block; padding: 10px 20px; background-color: #007bff; color: #fff; text-decoration: none; border-radius: 5px;'>Xác Nhận</a></p>
                    <p>Thân Gửi,</p>
                ";

                var message = new Message(new String[] { registerUser.Email }, "Xác Nhận Tài Khoản", htmlContent);
                _emailService.SendEmail(message);
                return StatusCode(StatusCodes.Status200OK,
                            new Response { Status = "Success", Message = "SuccessFully" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Message = tokenrole.Message, IsSucccess = false });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                var jwtToken = GetToken(authClaims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return Ok(new
            {
                model
            });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user != null)
            {
                var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
                if (!resetPassResult.Succeeded)
                {
                    foreach (var error in resetPassResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }
                return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = $"Password is Changed" });

            }
            return StatusCode(StatusCodes.Status400BadRequest,
                        new Response { Status = "Error", Message = "Couldnot send link to email, try again" });
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

        [HttpPost]
        [AllowAnonymous]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPasswork([Required] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var forgotpasswordlink = Url.Action("ResetPassword", "Authentication", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new String[] { user.Email }, "Forgot Password link", forgotpasswordlink!);
                _emailService.SendEmail(message);
                return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = $"Password Changed request is sent on Email {user.Email}. Please verify your email" });

            }
            return StatusCode(StatusCodes.Status400BadRequest,
                        new Response { Status = "Error", Message = "Couldnot send link to email, try again" });
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

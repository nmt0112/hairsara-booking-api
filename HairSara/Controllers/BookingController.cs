using HairSara.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage;
using Storage.Models;
using Storage.Models.Authentication.SignUp;
using Storage.Models.DTO;
using Storage.Services.ServiceBooking;
using Storage.Services.ServiceEmail;

namespace HairSara.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IEmailService _emailService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        public BookingController(IBookingService bookingService, IEmailService emailService, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _bookingService = bookingService;
            _emailService = emailService;
            _userManager = userManager;
            _context = context;
        }
        [HttpGet("ListBooking")]
        public async Task<ActionResult<List<Booking>>> GetAllAsync()
        {
            var bookings = await _bookingService.GetAllAsync();
            return Ok(bookings);
        }

        [HttpGet("Booking{id}")]
        public async Task<ActionResult<Booking>> GetByIdAsync(int id)
        {
            var booKing = await _bookingService.GetByIdAsync(id);
            if (booKing == null)
            {
                return NotFound();
            }
            return Ok(booKing);
        }
        [HttpGet("GetByCustomer")]
        public async Task<ActionResult<List<Booking>>> GetByCustomerAsync(int idCustomer)
        {
            var bookings = await _bookingService.GetByCustomerAsync(idCustomer);
            return Ok(bookings);
        }
        [HttpGet("GetByBarber")]
        public async Task<ActionResult<List<Booking>>> GetByBarberAsync(int idBarber)
        {
            var bookings = await _bookingService.GetByBarberAsync(idBarber);
            return Ok(bookings);
        }
        [HttpGet("GetByBarberChiNhanh")]
        public async Task<ActionResult<List<Booking>>> GetByBarberChiNhanhAsync(int idBarber, int idChiNhanh)
        {
            var bookings = await _bookingService.GetByBarberChiNhanhAsync(idBarber, idChiNhanh);
            return Ok(bookings);
        }

        [HttpPost("DatLich")]
        public async Task<IActionResult> AddBooking(int idCustomer, int idChiNhanh, int idBarber, int idDichVu, [FromBody] BookingDTO bookingDTO)
        {
            try
            {
                bookingDTO.IdCustomer = idCustomer;
                bookingDTO.IdChiNhanh = idChiNhanh;
                bookingDTO.IdDichVu = idDichVu;
                bookingDTO.IdBarber = idBarber;

                // Lấy thông tin người dùng từ Customer
                var idUserCustomer = await _context.Customer
                    .Where(c => c.Id == idCustomer)
                    .Select(c => c.IdUserCustomer)
                    .FirstOrDefaultAsync();
                var chiNhanh = await _context.ChiNhanh.FindAsync(idChiNhanh);
                var dichVu = await _context.DichVu.FindAsync(idDichVu);
                var barber = await _context.Barber.FindAsync(idBarber);
                var customer = await _context.Customer.FindAsync(idCustomer);
                var user = await _userManager.FindByIdAsync(idUserCustomer);
                // Thêm đặt lịch vào cơ sở dữ liệu và lấy Id sau khi đã thêm (chờ tác vụ hoàn thành)
                var addedBooking = await _bookingService.AddBookingAsync(bookingDTO);

                var customerEmail = user.Email;
                var confirmationLink = Url.Action(nameof(ConfirmBooking), "Booking", new { bookingId = addedBooking.Id }, Request.Scheme);
                var confirmationEmailTemplate = $@"
                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600""
                                                   style=""background-color:#ffffff;border:1px solid #dedede;border-radius:3px"">
                                                    <tbody>
                                                        <tr>
                                                            <td align=""center"" valign=""top"">

                                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%""
                                                                       style=""background-color:#96588a;color:#ffffff;border-bottom:0;font-weight:bold;line-height:100%;vertical-align:middle;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;border-radius:3px 3px 0 0"">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style=""padding:36px 48px;display:block"">
                                                                                <h1 style=""font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;font-size:30px;font-weight:300;line-height:150%;margin:0;text-align:left;color:#ffffff;background-color:inherit"">
                                                                                    Cảm ơn bạn đã lựa chọn HairSara
                                                                                </h1>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align=""center"" valign=""top"">

                                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td valign=""top"" style=""background-color:#ffffff"">

                                                                                <table border=""0"" cellpadding=""20"" cellspacing=""0"" width=""100%"">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td valign=""top"" style=""padding:48px 48px 32px"">
                                                                                                <div style=""color:#636363;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;font-size:14px;line-height:150%;text-align:left"">

                                                                                                    <p style=""margin:0 0 16px"">Xin chào {customer.NameCustomer},</p>
                                                                                                    <p style=""margin:0 0 16px"">
                                                                                                        Cảm ơn bạn đã lựa chọn dịch vụ của tôi. Được phục vụ quý khách là niềm vui của HairSara
                                                                                                    </p>

                                                                                                    <h2 style=""color:#96588a;display:block;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:0 0 18px;text-align:left"">
                                                                                                        Được đặt vào lúc: ({@DateTime.Now.ToString("dd/MM/yyyy HH:mm")})
                                                                                                    </h2>

                                                                                                    <div style=""margin-bottom:40px"">
                                                                                                        <table cellspacing=""0"" cellpadding=""6"" border=""1""
                                                                                                               style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;width:100%;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif"">
                                                                                                            <tfoot>
                                                                                                                <tr>
                                                                                                                    <th scope=""row"" colspan=""2""
                                                                                                                        style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left;border-top-width:4px"">
                                                                                                                        Chi Nhánh:
                                                                                                                    </th>
                                                                                                                    <td style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left;border-top-width:4px"">
                                                                                                                        <span>{chiNhanh.TenChiNhanh}&nbsp;</span>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <th scope=""row"" colspan=""2""
                                                                                                                        style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
                                                                                                                        Dịch Vụ:
                                                                                                                    </th>
                                                                                                                    <td style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
                                                                                                                        {dichVu.TenDichVu}
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <th scope=""row"" colspan=""2""
                                                                                                                        style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
                                                                                                                        Barber:
                                                                                                                    </th>
                                                                                                                    <td style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
                                                                                                                        {barber.NameBarber}
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <th scope=""row"" colspan=""2""
                                                                                                                        style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
                                                                                                                        Ngày hẹn:
                                                                                                                    </th>
                                                                                                                    <td style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
                                                                                                                        {bookingDTO.ThoiGianBatDau.ToString("dd/MM/yyyy")}
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <th scope=""row"" colspan=""2""
                                                                                                                        style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
                                                                                                                        Giờ hẹn:
                                                                                                                    </th>
                                                                                                                    <td style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
                                                                                                                        {bookingDTO.ThoiGianBatDau.ToString("HH:mm")}
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tfoot>
                                                                                                        </table>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <p><a href='{confirmationLink}' style='display: inline-block; padding: 10px 20px; background-color: #007bff; color: #fff; text-decoration: none; border-radius: 5px;'>Xác Nhận</a></p>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>

                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>

                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>";
                var message = new Message(new String[] { customerEmail }, "Xác Nhận Lịch Hẹn", confirmationEmailTemplate);
                _emailService.SendEmail(message);

                return StatusCode(StatusCodes.Status200OK,
                            new Response { Status = "Success", Message = "SuccessFully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Message = "Thất Bại", IsSucccess = false });
            }
        }

        [HttpGet("ConfirmBooking")]
        public async Task<IActionResult> ConfirmBooking(int bookingId)
        {
            try
            {
                // Thực hiện xác nhận đặt lịch
                await _bookingService.ConfirmBookingAsync(bookingId);

                return Redirect("https://localhost:7157/Home/Booking");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error confirming Booking: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking booking)
        {
            try
            {
                var existingBooking = await _bookingService.GetByIdAsync(id);

                if (existingBooking == null)
                {
                    return NotFound($"Booking with ID {id} not found");
                }

                booking.Id = id;
                await _bookingService.UpdateBookingAsync(booking);
                return Ok("Booking updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                var existingBooking = await _bookingService.GetByIdAsync(id);

                if (existingBooking == null)
                {
                    return NotFound($"Booking with ID {id} not found");
                }

                await _bookingService.DeleteBookingAsync(id);
                return Ok("Booking deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Storage.Services.ServiceBarber;
using Storage.Services.ServiceDanhMuc;

namespace HairSara.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BarberController : ControllerBase
    {
        private readonly IBarberService _service;

        public BarberController(IBarberService service)
        {
            _service = service;
        }

        [HttpGet("ListBarber")]
        public async Task<ActionResult<IEnumerable<Barber>>> GetAllAsync()
        {
            var barbers = await _service.GetAllAsync();
            return Ok(barbers);
        }

        [HttpGet("BarberId")]
        public async Task<ActionResult<Barber>> GetByIdAsync(int id)
        {
            var barber = await _service.GetByIdAsync(id);

            if (barber == null)
            {
                return NotFound();
            }

            return Ok(barber);
        }

        [HttpGet("BarberByChiNhanh")]
        public async Task<ActionResult<IEnumerable<Barber>>> GetByChiNhanhAsync(int chiNhanhId)
        {
            var barbers = await _service.GetByChiNhanhAsync(chiNhanhId);

            if (barbers == null || barbers.Count == 0)
            {
                return NotFound();
            }

            return Ok(barbers);
        }
    }

}

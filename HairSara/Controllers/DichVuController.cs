using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Storage.Models.DTO;
using Storage.Services.ServiceDichVu;

namespace HairSara.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DichVuController : ControllerBase
    {
        private readonly IDichVuService _service;

        public DichVuController(IDichVuService service)
        {
            _service = service;
        }

        [HttpGet("ListDichVu")]
        public async Task<ActionResult<List<DichVu>>> GetAll()
        {
            try
            {
                var dichVus = await _service.GetAllAsync();
                return Ok(dichVus);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting DichVus: {ex.Message}");
            }
        }

        [HttpGet("DichVu{id}")]
        public async Task<ActionResult<DichVu>> GetById(int id)
        {
            try
            {
                var dichVu = await _service.GetByIdAsync(id);

                if (dichVu == null)
                {
                    return NotFound($"DichVu with ID {id} not found");
                }

                return Ok(dichVu);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting DichVu: {ex.Message}");
            }
        }
        [HttpGet("GetByDanhMuc")]
        public async Task<ActionResult<List<DichVu>>> GetByDanhMucAsync(int idDanhMuc)
        {
            var dichvus = await _service.GetByDanhMucAsync(idDanhMuc);
            return Ok(dichvus);
        }
    }
}

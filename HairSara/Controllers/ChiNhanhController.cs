using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Storage.Services.ServiceChiNhanh;
using Storage.Services.ServiceViTri;

namespace HairSara.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChiNhanhController : ControllerBase
    {
        private readonly IChiNhanhService _service;

        public ChiNhanhController(IChiNhanhService service)
        {
            _service = service;
        }

        [HttpGet("ListChiNhanh")]
        public async Task<ActionResult<List<ChiNhanh>>> GetAllAsync()
        {
            var chiNhanhs = await _service.GetAllAsync();
            return Ok(chiNhanhs);
        }

        [HttpGet("ChiNhanh{id}")]
        public async Task<ActionResult<ChiNhanh>> GetByIdAsync(int id)
        {
            var chiNhanh = await _service.GetByIdAsync(id);
            if (chiNhanh == null)
            {
                return NotFound();
            }
            return Ok(chiNhanh);
        }
        [HttpGet("GetByViTri")]
        public async Task<ActionResult<List<ChiNhanh>>> GetByViTriAsync(int idViTri)
        {
            var chiNhanhs = await _service.GetByViTriAsync(idViTri);
            return Ok(chiNhanhs);
        }
    }
}

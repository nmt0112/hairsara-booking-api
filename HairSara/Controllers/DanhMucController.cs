using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Storage.Services.ServiceDanhMuc;

namespace HairSara.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DanhMucController : ControllerBase
    {
        private readonly IDanhMucService _service;

        public DanhMucController(IDanhMucService service)
        {
            _service = service;
        }

        [HttpGet("ListDanhMuc")]
        public async Task<ActionResult<IEnumerable<DanhMuc>>> GetAllAsync()
        {
            var danhmucs = await _service.GetAllAsync();
            return Ok(danhmucs);
        }
        [HttpGet("DanhMucId")]
        public async Task<ActionResult<DanhMuc>> GetByIdAsync(int id)
        {
            var danhMuc = await _service.GetByIdAsync(id);

            if (danhMuc == null)
            {
                return NotFound();
            }

            return Ok(danhMuc);
        }
    }
}

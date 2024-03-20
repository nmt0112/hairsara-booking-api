using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Storage.Models;
using Storage.Services.ServiceViTri;

namespace HairSara.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ViTriController : ControllerBase
    {
        private readonly IViTriService _service;

        public ViTriController(IViTriService service)
        {
            _service = service;
        }

        [HttpGet("ListViTri")]
        public async Task<ActionResult<IEnumerable<ViTri>>> GetAllAsync()
        {
            var vitris = await _service.GetAllAsync();
            return Ok(vitris);
        }
        [HttpGet("ViTriId")]
        public async Task<ActionResult<ViTri>> GetByIdAsync(int id)
        {
            var viTri = await _service.GetByIdAsync(id);

            if (viTri == null)
            {
                return NotFound();
            }

            return Ok(viTri);
        }
    }
}

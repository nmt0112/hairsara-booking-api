using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Storage.Models.DTO;
using Storage.Services.ServiceViTri;

namespace HairSara.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class ViTriAdminController : ControllerBase
    {
        private readonly IViTriService _service;

        public ViTriAdminController(IViTriService service)
        {
            _service = service;
        }

        [HttpPost("AddViTri")]
        public async Task<ActionResult<ViTri>> AddAsync([FromBody] ViTriDTO viTriDTO)
        {
            try
            {
                await _service.AddAsync(viTriDTO);
                return new JsonResult(new { Message = "ViTri added successfully", Data = viTriDTO });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding ViTri: {ex.Message}");
            }
        }

        [HttpDelete("DeleteViTri/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting ViTri: {ex.Message}");
            }
        }
        [HttpPut("UpdateViTri/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, ViTriDTO viTriDTO)
        {
            try
            {
                var existingViTri = (await _service.GetAllAsync())
                             .FirstOrDefault(v => v.Id == id);

                existingViTri.TinhThanhPho = viTriDTO.TinhThanhPho;
                await _service.UpdateAsync(existingViTri);
                return Ok("ViTri updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating ViTri: {ex.Message}");
            }
        }

    }
}

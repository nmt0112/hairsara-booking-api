using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Storage.Services.ServiceChiNhanh;
using System.Text.Json.Serialization;
using System.Text.Json;
using Storage.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace HairSara.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class ChiNhanhAdminController : ControllerBase
    {
        private readonly IChiNhanhService _service;

        public ChiNhanhAdminController(IChiNhanhService service)
        {
            _service = service;
        }

        [HttpPost("AddChiNhanh")]
        public async Task<IActionResult> AddChiNhanh(int idViTri, [FromBody] ChiNhanhDTO chiNhanhDTO)
        {
            try
            {
                chiNhanhDTO.IdViTri = idViTri;
                await _service.AddAsync(chiNhanhDTO);
                return new JsonResult(new { Message = "ChiNhanh added successfully with specified IdViTri.", Data = chiNhanhDTO });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding ChiNhanh: {ex.Message}");
            }
        }

        [HttpPut("UpdateChiNhanh{id}")]
        public async Task<ActionResult> UpdateChiNhanh(int id, [FromBody] ChiNhanhDTO chiNhanhDTO)
        {
            try
            {
                var existingChiNhanh = (await _service.GetAllAsync())
                             .FirstOrDefault(v => v.Id == id);

                // Cập nhật thông tin của existingViTri từ viTri
                existingChiNhanh.TenChiNhanh = chiNhanhDTO.TenChiNhanh;
                existingChiNhanh.DiaChi = chiNhanhDTO.DiaChi;
                existingChiNhanh.IdViTri = chiNhanhDTO.IdViTri;
                await _service.UpdateAsync(existingChiNhanh);
                return Ok("ChiNhanh updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating ChiNhanh: {ex.Message}");
            }
        }

        [HttpDelete("Delete{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting ChiNhanh: {ex.Message}");
            }
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Storage.Models.DTO;
using Storage.Services.ServiceDichVu;

namespace HairSara.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class DichVuAdminController : ControllerBase
    {
        private readonly IDichVuService _service;

        public DichVuAdminController(IDichVuService service)
        {
            _service = service;
        }

        [HttpPost("AddDichVu")]
        public async Task<IActionResult> AddDichVu(int idDanhMuc, [FromBody] DichVuDto dichVuDto)
        {
            try
            {
                dichVuDto.IdDanhMuc = idDanhMuc;
                await _service.AddAsync(dichVuDto);
                return new JsonResult(new { Message = "DichVu added successfully with specified IdDanhMuc.", Data = dichVuDto });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding DichVu: {ex.Message}");
            }
        }

        [HttpPut("UpdateDichVu{id}")]
        public async Task<ActionResult> UpdateDichVu(int id, [FromBody] DichVuDto dichVuDto)
        {
            try
            {
                var existingDichVu = (await _service.GetAllAsync())
                             .FirstOrDefault(v => v.Id == id);

                // Cập nhật thông tin của existingViTri từ viTri
                existingDichVu.TenDichVu = dichVuDto.TenDichVu;
                existingDichVu.MoTa = dichVuDto.MoTa;
                existingDichVu.Gia = dichVuDto.Gia;
                await _service.UpdateAsync(existingDichVu);
                return Ok("DichVu updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating DichVu: {ex.Message}");
            }
        }

        [HttpDelete("DeleteDichVu{id}")]
        public async Task<ActionResult> DeleteDichVu(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok("DichVu deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting DichVu: {ex.Message}");
            }
        }
    }
}

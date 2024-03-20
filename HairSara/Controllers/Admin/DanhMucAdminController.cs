using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Storage.Models.DTO;
using Storage.Services.ServiceDanhMuc;

namespace HairSara.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class DanhMucAdminController : ControllerBase
    {
        private readonly IDanhMucService _service;

        public DanhMucAdminController(IDanhMucService service)
        {
            _service = service;
        }

        [HttpPost("AddDanhMuc")]
        public async Task<ActionResult<DanhMuc>> AddAsync([FromBody] DanhMucDTO danhMucDTO)
        {
            try
            {
                await _service.AddAsync(danhMucDTO);
                return new JsonResult(new { Message = "DanhMuc added successfully", Data = danhMucDTO });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding DanhMuc: {ex.Message}");
            }
        }
        [HttpDelete("DeleteDanhMuc{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting DanhMuc: {ex.Message}");
            }
        }
        [HttpPut("UpdateDanhMuc{id}")]
        public async Task<IActionResult> UpdateAsync(int id, DanhMucDTO danhMucDTO)
        {
            try
            {
                var existingDanhMuc = (await _service.GetAllAsync())
                             .FirstOrDefault(v => v.Id == id);

                existingDanhMuc.TenDanhMuc = danhMucDTO.TenDanhMuc;
                existingDanhMuc.MoTaDanhMuc = danhMucDTO.MoTaDanhMuc;
                await _service.UpdateAsync(existingDanhMuc);
                return Ok("DanhMuc updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating DanhMuc: {ex.Message}");
            }
        }
    }
}

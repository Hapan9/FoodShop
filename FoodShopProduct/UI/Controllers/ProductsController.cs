using System;
using System.Threading.Tasks;
using BLL.Dto;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService service)
        {
            _productService = service;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                return new JsonResult(await _productService.GetWithScore(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return new JsonResult(await _productService.GetAllWithScore());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [CustomAuthorize("Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            try
            {
                await _productService.Create(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [CustomAuthorize("Administrator")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductDto dto)
        {
            try
            {
                await _productService.Update(id, dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [CustomAuthorize("Administrator")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _productService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
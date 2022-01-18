using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.Dto;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsInfoController : ControllerBase
    {
        private readonly IProductInfoService _productInfoService;

        public ProductsInfoController(IProductInfoService productInfoService)
        {
            _productInfoService = productInfoService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                return new JsonResult(await _productInfoService.Get(id));
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
                return new JsonResult(await _productInfoService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductInfoDto productInfoDto)
        {
            try
            {
                await _productInfoService.Create(productInfoDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductInfoDto productInfoDto)
        {
            try
            {
                await _productInfoService.Update(id, productInfoDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _productInfoService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{name}/Products")]
        public async Task<IActionResult> GetProducts([FromRoute] string name)
        {
            try
            {
                var productInfo = await _productInfoService.GetAll();
                var productIds = productInfo.Where(p => p.Name == name)
                    .Select(p => p.ProductId);

                return new JsonResult(productIds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpGet("Names")]
        public async Task<IActionResult> GetNames()
        {
            try
            {
                var productInfo = await _productInfoService.GetAll();
                var productNames = productInfo.Select(p => p.Name)
                    .Distinct();

                return new JsonResult(productNames);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
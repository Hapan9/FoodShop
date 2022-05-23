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
    public class ProductsScoreController : ControllerBase
    {
        private readonly IProductScoreService _productScoreService;

        public ProductsScoreController(IProductScoreService scoreService)
        {
            _productScoreService = scoreService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                return new JsonResult(await _productScoreService.Get(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return new JsonResult(await _productScoreService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [CustomAuthorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductScoreDto item)
        {
            try
            {
                await _productScoreService.Create(item);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [CustomAuthorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductScoreDto item)
        {
            try
            {
                await _productScoreService.Update(id, item);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [CustomAuthorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _productScoreService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id:guid}/User")]
        public async Task<IActionResult> GetUserScores([FromRoute] Guid id)
        {
            try
            {
                var userScores = (await _productScoreService.GetAll()).Where(ps => ps.UserId == id)
                    .Select(ps => ps.Score);
                return new JsonResult(userScores);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
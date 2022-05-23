using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserScoreController : ControllerBase
    {
        private readonly IUserScoreService _userScoreService;

        public UserScoreController(IUserScoreService userScoreService)
        {
            _userScoreService = userScoreService;
        }

        [HttpPost]
        public async Task<IActionResult> GetUsersScore([FromBody] IEnumerable<Guid> ids)
        {
            try
            {
                return new JsonResult(await _userScoreService.GetUsersScore(ids));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserScore([FromRoute] Guid id)
        {
            try
            {
                return new JsonResult(await _userScoreService.GetUserScore(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> UpdateUserScore([FromRoute] Guid id)
        {
            try
            {
                var result = await _userScoreService.UpdateUserScore(id);
                return new JsonResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
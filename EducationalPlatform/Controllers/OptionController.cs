using EducationalPlatform.DTO;
using EducationalPlatform.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class OptionController : ControllerBase
	{
			private readonly IOptionServices _optionServices;

			public OptionController(IOptionServices optionServices)
			{
				_optionServices = optionServices;
			}

			[HttpGet("{id}")]
			public async Task<IActionResult> GetOptionById(int id)
			{
				try
				{
					var option = await _optionServices.GetOptionById(id);
					return Ok(option);
				}
				catch (Exception ex)
				{
					return StatusCode(500, ex.Message);
				}
			}

			[HttpPost]
			public async Task<IActionResult> AddOption([FromBody] OptionDto optionDto)
			{
				try
				{
					await _optionServices.AddOption(optionDto.QuestionId, optionDto.OptionContent);
					return Ok("Option added successfully");
				}
				catch (Exception ex)
				{
					return StatusCode(500, ex.Message);
				}
			}

			[HttpPut("{id}")]
			public async Task<IActionResult> EditOption(int id, [FromBody] OptionDto optionDto)
			{
				try
				{
					await _optionServices.EditOption(id, optionDto.OptionContent);
					return Ok("Option updated successfully");
				}
				catch (Exception ex)
				{
					return StatusCode(500, ex.Message);
				}
			}

			[HttpDelete("{id}")]
			public async Task<IActionResult> RemoveOption(int id)
			{
				try
				{
					await _optionServices.RemoveOption(id);
					return Ok("Option removed successfully");
				}
				catch (Exception ex)
				{
					return StatusCode(500, ex.Message);
				}
			}
		}
	}

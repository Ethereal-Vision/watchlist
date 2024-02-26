using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WatchList.Controllers;

[ApiController]
[Route("[controller]")]
public class MediasController : ControllerBase
{
    private readonly IMediaLogic _mediaLogic;

    public MediasController(IMediaLogic mediaLogic)
    {
        _mediaLogic = mediaLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Media>> CreateAsync(MediaCreationDto dto)
    {
        try
        {
            Media media = await _mediaLogic.CreateAsync(dto);
            return Created($"/medias/{media.Id}", media);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] MediaUpdateDto dto)
    {
        try
        {
            await _mediaLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Media>> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            Media media = await _mediaLogic.GetByIdAsync(id);
            return Ok(media);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<Media>> GetAsync()
    {
        try
        {
            IEnumerable<Media> medias = await _mediaLogic.GetAsync();
            return Ok(medias);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]  
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await _mediaLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, e.Message);
        }
    }
}
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WatchList.Controllers;

[ApiController]
[Route("[controller]")]
public class MediaController : ControllerBase
{
    private readonly IMediaLogic _mediaLogic;

    public MediaController(IMediaLogic mediaLogic)
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
}
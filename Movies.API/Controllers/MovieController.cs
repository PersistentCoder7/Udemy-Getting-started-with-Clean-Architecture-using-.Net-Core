using System.Collections;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Commands;
using Movies.Application.Queries;
using Movies.Application.Responses;

namespace Movies.API.Controllers;

public class MovieController: ApiController
{
    private readonly IMediator _mediator;
    private readonly ILogger<MovieController> _logger;

    public MovieController(IMediator mediator, ILogger<MovieController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MovieResponse>> CreateMovie([FromBody] CreateMovieCommand createMovieCommand)
    {
        var movie = await _mediator.Send(createMovieCommand);
        return Ok(movie);
    }
    
    
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MovieResponse>>> GetMoviesByDirectorName(string directorName)
    {
        var query = new GetMoviesByDirectorNameQuery(directorName);
        var movies = await _mediator.Send(query);
        return Ok(movies);
    }
}
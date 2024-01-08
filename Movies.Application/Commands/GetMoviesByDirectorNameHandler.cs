using MediatR;
using Movies.Application.Mappers;
using Movies.Application.Queries;
using Movies.Application.Responses;
using Movies.Core.Repositories;

namespace Movies.Application.Commands;

public class GetMoviesByDirectorNameHandler: IRequestHandler<GetMoviesByDirectorNameQuery, IEnumerable<MovieResponse>>
{
    private readonly IMovieRepository _movieRepository;

    public GetMoviesByDirectorNameHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    public async Task<IEnumerable<MovieResponse>> Handle(GetMoviesByDirectorNameQuery request, CancellationToken cancellationToken)
    {
        var movies = await _movieRepository.GetMovieByDirectorName(request.DirectorName);
        var movieResponses = MovieMapper.Mapper.Map<IEnumerable<MovieResponse>>(movies);
        return movieResponses;
    }
}
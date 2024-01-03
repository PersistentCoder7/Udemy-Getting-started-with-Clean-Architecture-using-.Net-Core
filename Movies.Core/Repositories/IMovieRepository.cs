using Movies.Core.Entities;
using Movies.Core.Repositories.Base;

namespace Movies.Core.Repositories;

public interface IMovieRepository: IRepository<Movie>
{
    //Add any custom method that may be very specific to movies
    Task<IEnumerable<Movie>> GetMovieByDirectorName(string directorName);
}
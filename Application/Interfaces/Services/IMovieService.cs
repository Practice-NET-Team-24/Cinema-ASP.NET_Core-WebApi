using Application.DTOs;
namespace Application.Interfaces.Services
{
    public interface IMovieService
    {
        Task<MovieDTO> CreateMovieAsync(MovieDTO movieDTO);
        Task<MovieDTO?> DeleteMovieAsync(int movieId);
        Task<MovieDTO?> UpdateMovieAsync(MovieDTO movieDTO);
        Task<List<MovieDTO>> GetAllMoviesAsync();
        Task<List<MovieDTO>> GetMoviesAsync(int[] movieIds);
        Task<MovieDTO?> GetMovieAsync(int movieId);
        
    }
}

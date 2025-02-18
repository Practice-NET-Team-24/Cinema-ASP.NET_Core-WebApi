using Application.DTOs;
namespace Application.Interfaces.Services
{
    public interface ISessionService
    {
        Task<SessionDTO> CreateSessionAsync(SessionDTO sessionDTO, int movieId);
        Task<SessionDTO?> DeleteSessionAsync(int sessionId);
        Task<SessionDTO?> UpdateSessionAsync(SessionDTO sessionDTO);
        Task<List<SessionDTO>> GetAllMovieSessionsAsync(int movieId);
        Task<List<SessionDTO>> GetMovieSessionsByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<SessionDTO?> GetSessionAsync(int sessionId);

    }
}

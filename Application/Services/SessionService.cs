using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Application.Specifications;

namespace Application.Services
{
    public class SessionService : ISessionService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Session> _sessionRepository;

        public SessionService(IMapper mapper, IRepository<Session> sessionRepository)
        {
            _mapper = mapper;
            _sessionRepository = sessionRepository;
        }

        public async Task<SessionDTO> CreateSessionAsync(SessionDTO sessionDTO, int movieId)
        {
            var session = _mapper.Map<Session>(sessionDTO);
            session.MovieId = movieId;

            var addedSession = await _sessionRepository.InsertAsync(session);
            await _sessionRepository.SaveAsync();

            return _mapper.Map<SessionDTO>(addedSession);
        }

        public async Task<SessionDTO?> DeleteSessionAsync(int sessionId)
        {
            var deletedSession = await _sessionRepository.DeleteAsync(sessionId);
            if (deletedSession == null) return null;

            await _sessionRepository.SaveAsync();
            return _mapper.Map<SessionDTO>(deletedSession);
        }

        public async Task<SessionDTO?> UpdateSessionAsync(SessionDTO sessionDTO)
        {
            var existingSession = await _sessionRepository.GetFirstBySpecAsync(new Sessions.ById(sessionDTO.Id));
            if (existingSession == null) return null;

            _mapper.Map(sessionDTO, existingSession);

            var updatedSession = await _sessionRepository.UpdateAsync(existingSession);
            await _sessionRepository.SaveAsync();

            return _mapper.Map<SessionDTO>(updatedSession);
        }

        public async Task<List<SessionDTO>> GetAllMovieSessionsAsync(int movieId)
        {
            var sessions = await _sessionRepository.GetListBySpecAsync(new Sessions.AllByMovieId(movieId));
            return _mapper.Map<List<SessionDTO>>(sessions);
        }

        public async Task<List<SessionDTO>> GetMovieSessionsByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            var sessions = await _sessionRepository.GetListBySpecAsync(new Sessions.ByDateRange(fromDate, toDate));
            return _mapper.Map<List<SessionDTO>>(sessions);
        }

        public async Task<SessionDTO?> GetSessionAsync(int sessionId)
        {
            var session = await _sessionRepository.GetFirstBySpecAsync(new Sessions.ById(sessionId));
            return session == null ? null : _mapper.Map<SessionDTO>(session);
        }
    }
}

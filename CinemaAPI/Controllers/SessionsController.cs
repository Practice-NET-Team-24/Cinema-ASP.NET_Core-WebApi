using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }


        [HttpGet("movie/{id}")]
        public async Task<IActionResult> GetByMovieId(int id)
        {
            var sessions = await _sessionService.GetAllMovieSessionsAsync(id);
            if (sessions == null || sessions.Count == 0)
            {
                return NotFound("No sessions found for this movie.");
            }
            return Ok(sessions);
        }


        [HttpGet("date-range")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var sessions = await _sessionService.GetMovieSessionsByDateRangeAsync(from, to);
            if (sessions == null || sessions.Count == 0)
            {
                return NotFound("No sessions found in the provided date range.");
            }
            return Ok(sessions);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var session = await _sessionService.GetSessionAsync(id);
            if (session == null)
            {
                return NotFound($"Session with ID {id} not found.");
            }
            return Ok(session);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SessionDTO sessionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdSession = await _sessionService.CreateSessionAsync(sessionDTO, sessionDTO.MovieId);
            return CreatedAtAction(nameof(GetById), new { id = createdSession.Id }, createdSession);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SessionDTO sessionDTO)
        {
            if (id != sessionDTO.Id)
            {
                return BadRequest("Session ID mismatch.");
            }

            var updatedSession = await _sessionService.UpdateSessionAsync(sessionDTO);
            if (updatedSession == null)
            {
                return NotFound($"Session with ID {id} not found.");
            }

            return Ok(updatedSession);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedSession = await _sessionService.DeleteSessionAsync(id);
            if (deletedSession == null)
            {
                return NotFound($"Session with ID {id} not found.");
            }

            return Ok(deletedSession);
        }
    }
}

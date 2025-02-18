using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace CinemaAPI.Controllers;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AccountController(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = new User
        {
            Role = UserRole.Client,
            Email = model.Email,
            Name = model.Name,
            UserName = model.Email,
            NormalizedEmail = model.Email.ToUpper(),
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok("User registered successfully");
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Email.ToUpper());
        

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var authClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", user.Role.ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        return Unauthorized();
    }
    
    [Authorize]
    [HttpGet("protected")]
    public IActionResult Protected()
    {
        return Ok("You are authorized!");
    }
    
    [Authorize(Policy = "AdminOnly")]
    [HttpPost("admin-action")]
    public IActionResult AdminAction()
    {
        return Ok("Only admins can access this.");
    }
    
    [Authorize(Policy = "ClientOnly")]
    [HttpGet("client-action")]
    public IActionResult ClientAction()
    {
        return Ok("Only clients can access this.");
    }
}

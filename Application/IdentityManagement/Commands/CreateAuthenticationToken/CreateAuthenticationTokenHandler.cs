using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.IdentityManagement.Commands.CreateAuthenticationToken;

public class CreateAuthenticationTokenHandler : IRequestHandler<CreateAuthenticationTokenRequest, CreateAuthenticationTokenResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public CreateAuthenticationTokenHandler(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<CreateAuthenticationTokenResponse> Handle(CreateAuthenticationTokenRequest request, CancellationToken cancellationToken)
    {
        var validateUser = await _userRepository.ValidateUserAsync(request.Username, request.Password);

        if (validateUser.success)
        {
            var claims = GetClaims(validateUser.user!);
            var token = GenerateJwtToken(claims);

            return new CreateAuthenticationTokenResponse(true, token);
        }

        return new CreateAuthenticationTokenResponse(false, string.Empty);
    }

    public string GenerateJwtToken(List<Claim> claims)
    {
        var jwtConfig = _configuration.GetSection("JwtConfig");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtConfig["ExpiresInMinutes"]!));

        var token = new JwtSecurityToken(
            issuer: jwtConfig["ValidIssuer"]!,
            audience: jwtConfig["ValidAudience"]!,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(UserClaims.UserId, user.Id.ToString()),
            new Claim(UserClaims.UserName, user.UserName!),
        };

        return claims;
    }
}
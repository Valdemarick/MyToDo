using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyToDo.Application.Abstractions.Security;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Entities;
using MyToDo.Infrastructure.Constants;
using MyToDo.Infrastructure.Providers.Abstractions;

namespace MyToDo.Infrastructure.Security;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider;
    private readonly IPermissionProvider _permissionProvider;

    public JwtProvider(
        IOptions<JwtOptions> jwtOptions, 
        IDateTimeOffsetProvider dateTimeOffsetProvider, 
        IPermissionProvider permissionProvider)
    {
        _jwtOptions = jwtOptions.Value;
        _dateTimeOffsetProvider = dateTimeOffsetProvider;
        _permissionProvider = permissionProvider;
    }

    public async Task<string> GenerateTokenAsync(Member member)
    {
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, member.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, member.Email)
        };

        var memberPermissions = (await _permissionProvider.GetMemberPermissionsAsync(member.Id))
            .ToList();

        foreach (var memberPermission in memberPermissions)
        {
            claims.Add(new Claim(CustomClaims.Permissions, memberPermission.Name));
        }

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            System.DateTime.UtcNow.AddHours(_jwtOptions.ExpiresIn),
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}

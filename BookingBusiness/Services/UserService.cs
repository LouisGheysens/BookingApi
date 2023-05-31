using AutoMapper;
using BookingBusiness.Interface;
using BookingData;
using BookingData.Models;
using BookingDto.Models.User;
using BookingHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BookingBusiness.Services;
public class UserService : IUserService
{
    private readonly BookingDbContext _context;
    private readonly IMapper _mapper;

    public UserService(BookingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public string CreateToken(User request)
    {
        List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, request.UserName)
    };

        var key = new byte[256 / 8]; // 256 bits = 32 bytes
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(key);
        }

        var symmetricKey = new SymmetricSecurityKey(key);

        var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }


    public string Login(UserDto request)
    {
        var user = _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName).Result ?? throw new Exception("User not found");


        if (!IsPasswordValid(request.Password, user.HashedPassword))
        {
            throw new Exception("Wrong password");
        }

        return CreateToken(user);
    }

    public User Register(UserDto request)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = _mapper.Map<User>(request);
        SetUserHashedPassword(user, hashedPassword);

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

    private void SetUserHashedPassword(User user, string hashedPassword) => user.HashedPassword = hashedPassword;

    private bool IsPasswordValid(string password, string hashedPassword) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}

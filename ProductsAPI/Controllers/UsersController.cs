using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductsAPI.DTO;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController:ControllerBase
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly SignInManager<AppUser> _singInManager;
        private readonly IConfiguration _configuration;
        public UsersController(UserManager<AppUser> usermanager, SignInManager<AppUser> singInManager, IConfiguration configuration)
        {
            _usermanager=usermanager;
            _singInManager=singInManager;
            _configuration=configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult>CreateUser(UserDto model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new AppUser
                {
                    FullName = model.FullName,
                    UserName = model.UserName,
                    Email = model.Email,
                    DateAdded = DateTime.Now
                };
                var result = await _usermanager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return StatusCode(201);
                }
            
            return BadRequest(result.Errors);
        }
        [HttpPost("Login")]
        public async Task<IActionResult>Login(LoginDto model)
        {
            var user=await _usermanager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return BadRequest(new{message="email hatalı"});
            }
            var result=await _singInManager.CheckPasswordSignInAsync(user,model.Password,false);
            if (result.Succeeded)
            {
                return Ok( 
                    new {token=GenerateJWT(user)}
                );
            }
            return Unauthorized();
        }

        private object GenerateJWT(AppUser user)
        {
           var tokenHandler=new JwtSecurityTokenHandler();
           var key=Encoding.ASCII.GetBytes(_configuration.GetSection("Appsettings:Secret").Value ?? "");
           var tokenDescriptor=new SecurityTokenDescriptor
           {
               Subject=new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                     new Claim(ClaimTypes.Name,user.UserName ?? ""),
                }),
                Expires=DateTime.UtcNow.AddDays(1),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "mehmetyesil.com"
           };
           var token=tokenHandler.CreateToken(tokenDescriptor);
           return tokenHandler.WriteToken(token);
        }
    }
}
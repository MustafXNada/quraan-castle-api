using Microsoft.IdentityModel.Tokens;
using quraan_castle_api.Models;
using quraan_castle_api.Models_BLL.Requests;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace quraan_castle_api.Repositories
{
    public class AuthReposittory
    {
        private readonly quraancastledbContext _db;
        private readonly ILogger<AuthReposittory> _logger;
        public AuthReposittory(ILogger<AuthReposittory> logger) 
        {
            _db = new quraancastledbContext();
            _logger = logger;
        }

        public async Task<string> Login(string _email , string _password , string _userAgent)
        {
            try
            {
                string token = string.Empty;
                _password = _password.ComputeSHA256Hash();
                var user = _db.Users.Where(a=>a.Email== _email && a.Password == _password && a.IsActive).FirstOrDefault();
                if(user != null)
                {
                    
                    var tokenJwt = GetToken(new List<Claim>() 
                    {
                        new Claim("Uuid" , user.Uuid),
                        new Claim("Email" , user.Email),
                        new Claim("UserName" , user.Name),
                        new Claim("IsSubscriber" , user.IsSubscriber.ToString())
                    });

                    //add login into db 
                    _db.Userlogins.Add(new Userlogin()
                    {
                         CreatedAt = DateTime.UtcNow,
                         Token = token,
                         UserId = user.Id,
                    });         
                    await  _db.SaveChangesAsync();
                    return new JwtSecurityTokenHandler().WriteToken(tokenJwt);
                }

                
                return token;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> EmailIsExist(string _email)
        {
            try
            {
                return _db.Users.Any(a => a.Email.Trim().ToLower() == _email.Trim().ToLower());
            }
            catch(Exception ex) { throw; }
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            try
            {
                var model = new User()
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password= request.Password.ComputeSHA256Hash(),
                    Gender= request.Gender,
                    IsActive = true,
                    IsSubscriber = false,
                    CreatedAt = DateTime.UtcNow,
                    Uuid = Guid.NewGuid().ToString(),
                    
                };
                _db.Users.Add(model);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task ChnagePassword(ChangePasswordRequest request)
        {
            var user = _db.Users.Where(a => a.Uuid == request.userUuid && a.Email == request.userEmail).FirstOrDefault();
            if(user != null)
            {
                if(user.Password == request.userCurrentPassword.ComputeSHA256Hash() )
                {
                    user.Password = request.userPassword.ComputeSHA256Hash();
                    _db.Users.Update(user);
                    _db.SaveChanges();
                }
            }
            else
                throw new Exception("user not found");

        }
        private JwtSecurityToken GetToken(List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SystemSession.configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(4),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)

                );

            return token;
        }

    }
}

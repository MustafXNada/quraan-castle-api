using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.IO;
using quraan_castle_api.Models_BLL.Requests;
using quraan_castle_api.Repositories;
using quraan_castle_api.Services;

namespace quraan_castle_api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly AuthReposittory _auth;
        private readonly EmailService _emailService;
        public AuthController(ILogger<AuthController> logger , 
            AuthReposittory auth,
            EmailService emailService) 
        {
            _logger = logger;
            _auth = auth;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest request )
        {
            try
            {
                _logger.LogInformation("new request "+ DateTime.UtcNow.ToString() , request);
                var token = await _auth.Login(request.email, request.password, "");
                if(string.IsNullOrEmpty(token))
                {
                    _logger.LogInformation("401", "invalid credentials");
                    return ApiResponses.Fail("401", "invalid credentials");
                }
                return ApiResponses.Success(token.ToString());
            }
            catch(Exception ex) 
            {
                _logger.LogError("401-invalid credentials" , ex);
                return ApiResponses.Fail("01", ex.Message);
            }
        }

        [HttpPost]
        [Route("EmailIsExist")]
        public async Task<IActionResult> EmailIsExist(EmailIsExistRequest request)
        {
            try
            {
                return ApiResponses.Success(await _auth.EmailIsExist(request.Email));
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApiResponses.Fail("01", ex.Message);
            }
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                var emailIsExist = await _auth.EmailIsExist(request.Email);
                if (emailIsExist)
                    return ApiResponses.Fail("02", "email already exist");
                var isSuccess = await _auth.Register(request);
                if(!isSuccess)
                {
                    return ApiResponses.Fail("NA", "failed process please refresh page and try again :) ");
                }
                return ApiResponses.Success(isSuccess.ToString() , "Welecom on our quraan castle family");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message , ex);
                return ApiResponses.Fail("01" , ex.Message);
            }
        }


        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(EmailIsExistRequest request)
        {
            try
            {
                var password = GlobalService.GenerateRandomPassword(10);
                await _emailService.SendEmail(new UserEmailOptions()
                {
                    ToEmails = new List<string>() { request.Email },
                    Body = "<strong>Password : " + password + "</strong>  <br/>please update it ASPA",
                    Subject = "Reset Password"
                });

                return ApiResponses.Success(true, "New Password is Generated Spacially to reset your account, please chnage it after first login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApiResponses.Fail("01", ex.Message);
            }
        }

        [HttpPost]
        [Route("ChnagePassword")]
        
        public async Task<IActionResult> ChnagePassword(ChangePasswordRequest request)
        {
            try
            {
                await _auth.ChnagePassword(request);

                return ApiResponses.Success(true, "New Password is Generated Spacially to reset your account, please chnage it after first login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApiResponses.Fail("01", ex.Message);
            }
        }



    }
}

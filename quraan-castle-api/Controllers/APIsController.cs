using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quraan_castle_api.Models;
using quraan_castle_api.Models_BLL;
using quraan_castle_api.Models_BLL.Requests;
using quraan_castle_api.Models_BLL.Responses;
using System.Net;
using System.Security.Claims;

namespace quraan_castle_api.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class APIsController : ControllerBase
    {
        private readonly quraancastledbContext _db;
        private readonly ILogger<APIsController> _logger;
        private UserInfo _user = null;
        public APIsController(quraancastledbContext db, ILogger<APIsController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpPost]
        [Route("Plans")]
        [ProducesResponseType(typeof(ApiResponseModel<PlansResponse>), 200)]
        public IActionResult Plans() 
        {
            try
            {
                this.SetUserInfo();
                var data = _db.Plans
                    .Where(a => a.IsActive)
                    .Select(a=> new PlanModel() 
                    {
                        id = a.Id,
                        name = a.Name,
                        description = a.Desc??"",
                        price = a.Price
                    }).ToList();
                if(data.Count > 0)
                    return ApiResponses.Success<PlansResponse>(new PlansResponse() { models = data} );
                else
                    return ApiResponses.Fail("00" , "No Data Founded");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, ex);
                return ApiResponses.Fail("01",ex.Message);
            }
        }

        [HttpPost]
        [Route("Teachers")]
        [ProducesResponseType(typeof(ApiResponseModel<TeachersResponse>), 200)]
        public IActionResult Teachers()
        {
            try
            {
                var data = _db.Mentors
                    .Where(a => a.IsActive && a.IsFeatured)
                    .OrderBy(a => a.Order)
                    .Select(a=> new TeacherModel()
                    {
                        name = a.Name,
                        gender = a.Gender,
                        email = a.Email,
                        order = a.Order??0
                    })
                    .ToList();
                if (data.Count > 0)
                    return ApiResponses.Success<TeachersResponse>(new TeachersResponse() { models = data});
                else
                    return ApiResponses.Fail("00", "No Data Founded");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApiResponses.Fail("01",ex.Message);
            }
        }

        [HttpPost]
        [Route("Videos")]
        [ProducesResponseType(typeof(ApiResponseModel<VideosResponse>), 200)]
        public IActionResult Videos()
        {
            try
            {
                var data = _db.Videos
                    .Where(a => a.IsFeatured)
                    .OrderBy(a=>a.Order)
                    .Select(a=> new VideoModel()
                    {
                        title = a.Name,
                        bio = a.Desc,
                        path = "",
                        order = a.Order??0
                    })
                    .ToList();
                if (data.Count > 0)
                    return ApiResponses.Success<VideosResponse>(new VideosResponse() { models = data});
                else
                    return ApiResponses.Fail("00", "No Data Founded");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return   ApiResponses.Fail( "01", ex.Message);
            }
        }

        [HttpPost]
        [Route("Review")]
        [ProducesResponseType(typeof(ApiResponseModel<bool>), 200)]
        public IActionResult Review(ReviewRequest request)
        {
            try
            {
                this.SetUserInfo();
                var userId = _db.Users.Where(a => a.Uuid == _user.Uuid).Select(a => a.Id).FirstOrDefault();
                _db.Reviews.Add(new Models.Review()
                {
                    Comment = request.comment,
                    TeacherId = request.teacherId,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                });
                _db.SaveChanges();
                return ApiResponses.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApiResponses.Fail("01", ex.Message);
            }
        }

        [HttpPost]
        [Route("Rate")]
        [ProducesResponseType(typeof(ApiResponseModel<bool>), 200)]
        public IActionResult Rate(RateRequest request)
        {
            try
            {
                this.SetUserInfo();
                var userId = _db.Users.Where(a => a.Uuid == _user.Uuid).Select(a => a.Id).FirstOrDefault();
                _db.Rates.Add(new Models.Rate()
                {
                    Rate1 = request.Rate,
                    TeacherId = request.teacherId,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                });
                _db.SaveChanges();
                return ApiResponses.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApiResponses.Fail("01", ex.Message);
            }
        }


        private void SetUserInfo()
        {

            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    var userClaims = identity.Claims;
                    _user = new UserInfo
                    {
                        Name = userClaims.FirstOrDefault(x => x.Type == "UserName")?.Value,
                        Email = userClaims.FirstOrDefault(x => x.Type == "Email")?.Value,
                        Uuid = userClaims.FirstOrDefault(x => x.Type == "Uuid")?.Value,
                        IsSubscriber = userClaims.FirstOrDefault(x => x.Type == "IsSubscriber")?.Value,

                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

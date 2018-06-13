using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AskME.Core.Entities;
using AskME.Core.ServiceInterface;
using AskME.WebApp.Models;
using AskME.WebApp.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AskME.WebApp.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        IUserInfoService _userInfoService;

        public UserController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }


        [Route("login", Name = "Login")]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (returnUrl == null)
                {
                    return Redirect("/");
                }
            }
            TempData["returnUrl"] = returnUrl;
            return View();
        }


        [Route("signup", Name = "SignUp")]
        public IActionResult SignUp()
        {
            return View();
        }


        [Route("logout", Name = "LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginViewModel">登录信心</param>
        /// <param name="returnUrl">返回URL</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Route("auth")]
        public async Task<JsonResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            ResultModel result = new ResultModel();
            UserInfo user = _userInfoService.UserIsExists(loginViewModel.UserName, loginViewModel.Password);
            if (user != null)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Uri, user.Icon ?? string.Empty));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(3)) });
                if (returnUrl == null)
                {
                    returnUrl = TempData["returnUrl"]?.ToString();
                }
                result.Status = System.Net.HttpStatusCode.OK;
                result.Msg = "Success";
            }
            else
            {
                result.Status = System.Net.HttpStatusCode.NotFound;
                result.Msg = "用户名或密码错误";
            }
            return Json(result);
        }




        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [Route("register")]
        public JsonResult Register(RegisterModel register)
        {
            ResultModel result = new ResultModel();
            if (_userInfoService.AddUserInfo(register.UserName, register.Email, register.Password, out string msg))
            {
                result.Status = System.Net.HttpStatusCode.OK;
                result.Data = "";
                result.Msg = msg;
            }
            else
            {
                result.Status = System.Net.HttpStatusCode.NotFound;
                result.Data = "";
                result.Msg = "404,not found";
            }
            return Json(result);
        }




        [Route("")]
        public IActionResult Users(int page = 1)
        {
            page = page <= 0 ? 1 : page;
            ViewBag.Users = _userInfoService.PageList(page, 30, u => u.UserName);
            ViewBag.Page = page;
            return View();
        }


        /// <summary>
        /// 添加对标签的关注
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("add/follow")]
        public IActionResult AddFollow(int id)
        {
            ResultModel result = new ResultModel();
            int userId = Convert.ToInt32(User.Claims.Single(c => c.Type == ClaimTypes.Sid).Value);
            if (_userInfoService.AddFollowTag(userId, id))
            {
                result.Status = System.Net.HttpStatusCode.OK;
                result.Msg = "Success";
            }
            else
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Msg = "404,not found";
            }
            return Json(result);
        }



        [HttpPost]
        [Route("add/tag")]
        public JsonResult AddTag(string tagName, string des)
        {
            ResultModel result = new ResultModel();
            Tag tag = new Tag { TagName = tagName.ToLower().Trim(), Description = des };
            if (_userInfoService.AddTag(tag))
            {
                result.Status = System.Net.HttpStatusCode.OK;
                result.Msg = "success";
            }
            else
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Msg = "error";
            }
            return Json(result);

        }
    }
}
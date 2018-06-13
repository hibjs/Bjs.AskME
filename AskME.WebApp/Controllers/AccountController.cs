using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AskME.Core.Entities;
using AskME.Core.Interfaces;
using AskME.Core.ServiceInterface;
using AskME.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AskME.WebApp.Controllers
{
    [Route("users")]
    public class AccountController : Controller
    {
        IUserInfoService _userInfoService;
        IQuestionService _questionService;
        IUploadImg _uploadImg;
        IBlogService _blogService;
        public AccountController(IUserInfoService userInfoService,
            IQuestionService questionService,
            IUploadImg uploadImg,
            IBlogService blogService)
        {
            _userInfoService = userInfoService;
            _questionService = questionService;
            _uploadImg = uploadImg;
            _blogService = blogService;
        }

        [Route("{id}/{userName}")]
        public IActionResult Users()
        {
            int id = Convert.ToInt32(RouteData.Values["id"]);
            List<Question> questions = _questionService.GetQuestionsByUserId(id);
            List<Question> answers = _questionService.GetAnswersByUserId(id);
            UserInfo user = _userInfoService.GetUser(id);
            ViewBag.QuestionCount = questions.Count;
            ViewBag.AnswerCount = answers.Count;
            ViewBag.Tags = _userInfoService.GetUserFollowedTags(id);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Questions = questions.Take(5).OrderBy(q => q.Count);
                ViewBag.Answers = answers.Take(5).OrderBy(q => q.Up);
            }
            else
            {
                ViewBag.Questions = questions;
                ViewBag.Answers = answers;
            }
            ViewBag.UserInfo = user;
            return View();
        }

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <returns></returns>

        [Authorize]
        [Route("edit/{id}")]
        public IActionResult EditUserInfo()
        {
            ViewBag.UserInfo = _userInfoService.GetUser(Convert.ToInt32(User.Claims.Single(c => c.Type == ClaimTypes.Sid).Value));
            return View();
        }


        [Authorize]
        [Route("{id}/{userName}/activity")]
        public IActionResult Activity(string tab)
        {
            int id = Convert.ToInt32(User.Claims.Single(c => c.Type == ClaimTypes.Sid).Value);
            if (tab == "question")
            {
                ViewBag.Questions = _questionService.GetQuestionsByUserId(id);
                return View("QuestionTab");
            }
            else if (tab == "answer")
            {
                ViewBag.Questions = _questionService.GetAnswersByUserId(id);
                return View("AnswerTab");
            }
            else if (tab == "blogs")
            {
                ViewBag.Blogs = _blogService.GetBlogsByUserId(id);
                return View("BlogsTab");
            }
            return View();

        }



        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="address"></param>
        /// <param name="bio"></param>
        /// <param name="about"></param>
        /// <param name="telNumber"></param>
        /// <param name="wechat"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("edit")]
        public JsonResult EditUserInfo(string userName, string address, string bio, string about, string telNumber, string wechat, string qq)
        {
            ResultModel result = new ResultModel();
            UserInfo userInfo = _userInfoService.GetUser(Convert.ToInt32(User.Claims.Single(c => c.Type == ClaimTypes.Sid).Value));
            userInfo.UserName = userName ?? string.Empty;
            userInfo.Address = address ?? string.Empty;
            userInfo.Bio = bio ?? string.Empty;
            userInfo.SelfInfo = about ?? string.Empty;
            userInfo.TelNumber = telNumber ?? string.Empty;
            userInfo.WeChat = wechat ?? string.Empty;
            userInfo.QQ = qq ?? string.Empty;
            if (_userInfoService.Edit(userInfo))
            {
                result.Status = System.Net.HttpStatusCode.OK;
                result.Msg = "Success";
            }
            else
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Msg = "error";
            }
            return Json(result);
        }

        /// <summary>
        /// 上传用户图像
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("icon/upload")]
        public JsonResult UploadUserIcon()
        {
            ResultModel result = new ResultModel();
            UserInfo userInfo = _userInfoService.GetUser(Convert.ToInt32(User.Claims.Single(c => c.Type == ClaimTypes.Sid).Value));
            IFormFile file = Request.Form.Files.FirstOrDefault();
            userInfo.Icon = _uploadImg.UploadSingleFile(file);
            if (_userInfoService.Edit(userInfo))
            {
                result.Status = System.Net.HttpStatusCode.OK;
                result.Data = userInfo.Icon;
                result.Msg = "Success";
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
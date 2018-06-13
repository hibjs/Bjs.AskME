using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AskME.Core.Entities;
using AskME.Core.ServiceInterface;
using AskME.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AskME.WebApp.Controllers
{
    [Route("question")]
    public class QuestionController : Controller
    {
        IQuestionService _questionService;
        IUserInfoService _userInfoService;
        ITagService _tagService;
        public QuestionController(
            IUserInfoService userInfoService,
            IQuestionService questionService,
            ITagService tagService)
        {
            _questionService = questionService;
            _userInfoService = userInfoService;
            _tagService = tagService;
        }

        /// <summary>
        /// 提问建议页
        /// </summary>
        /// <returns></returns>
        [Route("advince", Name = "Advince")]
        public IActionResult Advince()
        {
            return View();
        }

        /// <summary>
        /// 提问页
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("ask", Name = "Ask")]
        public IActionResult Ask()
        {
            return View();
        }

        /// <summary>
        /// 问题列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("{tab}", Name = "Question")]
        public IActionResult Question(int page = 1)
        {
            string tab = RouteData.Values["tab"].ToString();
            ViewBag.Count = _questionService.Count();
            ViewBag.TopTags = _tagService.GetTopTags(10);
            if (tab == "new")
            {
                ViewBag.Questions = _questionService.PageList<DateTime>(page, 30, q => q.SubTime);
            }
            else if (tab == "hot")
            {
                ViewBag.Questions = _questionService.PageList(page, 30, q => true).OrderByDescending(q => q.Count);
            }
            else if (tab == "unanswered")
            {
                ViewBag.Questions = _questionService.PageList<int>(page, 30, q => q.Count).Where(q => q.Count == 0);
            }
            ViewBag.Page = page;
            return View();
        }


        /// <summary>
        /// 新增问题
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("")]
        public JsonResult AddQuestion(string title, string description, string ids)
        {
            ResultModel result = new ResultModel();
            int userId = Convert.ToInt32(User.Claims.Single(c => c.Type == ClaimTypes.Sid).Value);
            string[] sids = ids.Split(',').ToArray();
            Question question = new Question { Title = title, Description = description, UserInfoId = userId };
            if (_questionService.Add(question, sids))
            {
                result.Data = "";
                result.Status = System.Net.HttpStatusCode.OK;
                result.Msg = "Success";
            }
            else
            {
                result.Data = "";
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Msg = "error";
            }
            return Json(result);
        }

        /// <summary>
        /// 问答详情
        /// </summary>
        /// <returns></returns>
        [Route("{id:int}")]
        public IActionResult QuestionDetails()
        {
            int id = Convert.ToInt32(RouteData.Values["id"]);
            Question question = _questionService.GetQuestion(id);
            ViewBag.Question = question;
            ViewData["Title"] = question.Title.Length > 16 ? question.Title.Substring(0, 16) + "..." : question.Title;
            return View();
        }


        [Authorize]
        [HttpPost]
        [Route("answer")]
        public JsonResult AddAnswer(int qId, string answerContent)
        {
            ResultModel result = new ResultModel();
            UserInfo userInfo = _userInfoService.GetUser(Convert.ToInt32(User.Claims.Single(c => c.Type == ClaimTypes.Sid).Value));
            List<Answer> answers = new List<Answer>() {
                 new Answer { AnserContent = answerContent, Adoption = false, Up = 0, Down = 0, QuestionId = qId,UserId=userInfo.Id }
            };
            userInfo.Answers = answers;
            if (_userInfoService.Edit(userInfo))
            {
                result.Status = System.Net.HttpStatusCode.OK;
                result.Msg = "Success";
            }
            else
            {
                result.Status = System.Net.HttpStatusCode.OK;
                result.Msg = "Success";
            }
            return Json(result);
        }


        /// <summary>
        /// 通过Tag进入
        /// </summary>
        /// <returns></returns>
        [Route("tagged/{tag}")]
        public IActionResult QuestionTagged()
        {
            string tag = RouteData.Values["tag"].ToString();
            ViewBag.Questions = _questionService.PageListByTagged<DateTime>(1, 30, q => q.SubTime, tag);
            Tag tagObj = _tagService.GetTagByTagName(tag);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Follow = _userInfoService.HasFollowed(tag, Convert.ToInt32(User.Claims.Single(c => c.Type == ClaimTypes.Sid).Value));
            }
            else
            {
                ViewBag.Follow = false;
            }
            ViewBag.Tag = tagObj;
            ViewData["Title"] = tag;
            return View();
        }
    }
}
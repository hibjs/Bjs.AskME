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
    [Route("blogs")]
    public class BlogController : Controller
    {
        IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [Route("", Name = "Blogs")]
        public IActionResult Blogs(int page = 1)
        {
            page = page <= 0 ? 1 : page;
            ViewBag.Blogs = _blogService.PageList<DateTime>(page, 30, b => b.SubTime);
            ViewBag.Page = page;
            return View();
        }

        [Authorize]
        [Route("add")]
        public IActionResult Add()
        {
            return View();
        }


        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("add/post")]
        public JsonResult Add(string title, string content, string ids)
        {
            ResultModel result = new ResultModel();
            int userId = Convert.ToInt32(User.Claims.Single(c => c.Type == ClaimTypes.Sid).Value);
            string[] sids = ids.Split(',').ToArray();
            Blog blog = new Blog { Title = title, Content = content, UserInfoId = userId };
            if (_blogService.AddBlog(blog, sids))
            {
                result.Status = System.Net.HttpStatusCode.OK;
                result.Msg = "Success";
            }
            else
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Msg = "Error";
            }
            return Json(result);
        }



        [Route("{id}")]
        public IActionResult BlogDetail()
        {
            int id = Convert.ToInt32(RouteData.Values["id"]);
            Blog blog = _blogService.GetBlogById(id);
            ViewData["Title"] = blog.Title.Length > 20 ? blog.Title.Substring(0, 20) : blog.Title;
            ViewBag.Blog = blog;
            return View();
        }
    }
}
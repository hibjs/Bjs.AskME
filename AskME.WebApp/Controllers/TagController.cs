using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskME.Core.Entities;
using AskME.Core.ServiceInterface;
using AskME.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AskME.WebApp.Controllers
{
    [Route("tags")]
    public class TagController : Controller
    {
        ITagService _topicService;
        public TagController(ITagService topicService)
        {
            _topicService = topicService;
        }


        [Route("{key}")]
        public JsonResult Search()
        {
            ResultModel result = new ResultModel();
            string key = Convert.ToString(RouteData.Values["key"]);
            List<Tag> topics = _topicService.SearchByTag(key);
            if (topics.Count > 0)
            {
                result.Status = System.Net.HttpStatusCode.OK;
                result.Msg = "Success";
                result.Data = topics;
            }
            else
            {
                result.Status = System.Net.HttpStatusCode.NotFound;
                result.Msg = "404，no found";
                result.Data = "";
            }
            return Json(result);
        }


        /// <summary>
        /// 标签首页
        /// </summary>
        /// <returns></returns>
        [Route("", Name = "Tags")]
        public IActionResult Tags(int page = 1)
        {
            ViewBag.Tags = _topicService.PageList(1, 30, t => t.TagName);
            ViewBag.Page = page;
            return View();
        }
    }
}
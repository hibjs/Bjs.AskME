using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskME.Core.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace AskME.WebApp.Controllers
{
    [Route("")]
    public class IndexController : Controller
    {
        IQuestionService _questionService;
        public IndexController(IQuestionService questionService)
        {
            _questionService = questionService;
        }


        [Route("")]
        public IActionResult Index()
        {

            return View();
        }
    }
}
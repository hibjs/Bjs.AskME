/*
* Copyright (C) 2018 河南唐思互联网科技有限公司
* 版权所有
*
* 文件名：BlogService
* 文件功能描述：
*
* Author:bixiaoshan
* Date: 2018/4/19 15:00:13
*
* Version:V1.0.0
*/
using AskME.Core.Entities;
using AskME.Core.RepositoryInterface;
using AskME.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AskME.Core.Services
{
    public class BlogService : IBlogService
    {
        IBlogRepository _blogRepository;
        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public bool AddBlog(Blog blog, string[] ids) => _blogRepository.AddBlog(blog, ids);
        public List<Blog> PageList<s>(int pageIndex, int pageSize, Expression<Func<Blog, s>> orderByLambda) =>
               _blogRepository.PageList(pageIndex, pageSize, orderByLambda);

        public Blog GetBlogById(int id) => _blogRepository.GetBlogById(id);

        public List<Blog> GetBlogsByUserId(int id) => _blogRepository.List(b => b.UserInfoId == id).ToList();


    }
}

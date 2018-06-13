/*
* Copyright (C) 2018 河南唐思互联网科技有限公司
* 版权所有
*
* 文件名：IBlogService
* 文件功能描述：
*
* Author:bixiaoshan
* Date: 2018/4/19 15:01:30
*
* Version:V1.0.0
*/
using AskME.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AskME.Core.ServiceInterface
{
    public interface IBlogService
    {
        bool AddBlog(Blog blog, string[] ids);
        List<Blog> PageList<s>(int pageIndex, int pageSize, Expression<Func<Blog, s>> orderByLambda);
        Blog GetBlogById(int id);
        List<Blog> GetBlogsByUserId(int id);
    }
}

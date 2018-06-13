/*
* Copyright (C) 2018 河南唐思互联网科技有限公司
* 版权所有
*
* 文件名：BlogRepository
* 文件功能描述：
*
* Author:bixiaoshan
* Date: 2018/4/19 14:58:38
*
* Version:V1.0.0
*/
using AskME.Core.Entities;
using AskME.Core.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AskME.Infrastructure.Data
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(AskMEDbContext dbContext) : base(dbContext) { }


        /// <summary>
        /// 新增专栏
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool AddBlog(Blog blog, string[] ids)
        {
            _dbContext.Blogs.Add(blog);
            List<BlogTag> blogTags = new List<BlogTag>();
            foreach (string id in ids)
            {
                blogTags.Add(new BlogTag { BlogId = blog.Id, TagId = Convert.ToInt32(id) });
            }
            _dbContext.BlogTags.AddRange(blogTags);
            return _dbContext.SaveChanges() > 0;
        }


        /// <summary>
        /// 分页查询Question
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByLambda"></param>
        /// <returns></returns>
        public List<Blog> PageList<s>(int pageIndex, int pageSize, Expression<Func<Blog, s>> orderByLambda)
        {
            List<Blog> blogs = _dbContext.Blogs.Take(pageSize).Skip((pageIndex - 1) * pageSize)
                .OrderByDescending(orderByLambda).ToList();
            foreach (Blog b in blogs)
            {
                List<BlogTag> blogTags = _dbContext.BlogTags.Where(bt => bt.BlogId == b.Id).ToList();
                List<Tag> tags = new List<Tag>();
                foreach (BlogTag bt in blogTags)
                {
                    tags.Add(_dbContext.Tags.Where(t => t.Id == bt.TagId).FirstOrDefault());
                }
                b.Tags = tags;
                UserInfo userInfo = _dbContext.UserInfos.Where(u => u.Id == b.UserInfoId).FirstOrDefault();
                b.UserName = userInfo.UserName;
                b.Icon = userInfo.Icon;
            }
            return blogs;
        }


        /// <summary>
        /// 根据Id查询Blog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Blog GetBlogById(int id)
        {
            Blog blog = _dbContext.Blogs.Where(b => b.Id == id).FirstOrDefault();

            List<BlogTag> blogTags = _dbContext.BlogTags.Where(bt => bt.BlogId == blog.Id).ToList();
            List<Tag> tags = new List<Tag>();
            foreach (BlogTag bt in blogTags)
            {
                tags.Add(_dbContext.Tags.Where(t => t.Id == bt.TagId).FirstOrDefault());
            }
            blog.Tags = tags;
            UserInfo userInfo = _dbContext.UserInfos.Where(u => u.Id == blog.UserInfoId).FirstOrDefault();
            blog.UserName = userInfo.UserName;
            blog.Icon = userInfo.Icon;
            return blog;
        }



        public List<Blog> GetBlogsById(int id)
        {
            return _dbContext.Blogs.Where(b => b.UserInfoId == id).ToList();
        }
    }
}

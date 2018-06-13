using AskME.Core.Entities;
using AskME.Core.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AskME.Infrastructure.Data
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(AskMEDbContext dbContext) : base(dbContext) { }

        public List<Tag> SearchByKeyWord(string key) => _dbContext.Tags.Where(t => t.TagName.Contains(key)).ToList();


        public List<Tag> PageList<s>(int pageIndex, int pageSize, Expression<Func<Tag, s>> orderByLambda)
        {
            List<Tag> tags = _dbContext.Tags.Take(pageSize).Skip((pageIndex - 1) * pageSize).OrderBy(orderByLambda).ToList();

            foreach (Tag tag in tags)
            {
                tag.Count = _dbContext.QuestionTags.Where(qt => qt.TagId == tag.Id).Count();
            }
            return tags;
        }

        /// <summary>
        /// 根据tagname 获取Tag
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public Tag GetTagByTagName(string tagName) => _dbContext.Tags.Where(t => t.TagName == tagName).FirstOrDefault();



        public List<Tag> GetTopTags(int count)
        {
            List<Tag> tags = _dbContext.Tags.ToList();
            foreach (Tag tag in tags)
            {
                tag.Count = _dbContext.QuestionTags.Where(qt => qt.TagId == tag.Id).Count();
            }
            return tags.Take(count).OrderByDescending(t => t.Count).ToList();
        }
    }
}

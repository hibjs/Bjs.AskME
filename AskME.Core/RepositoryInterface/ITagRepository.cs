using AskME.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AskME.Core.RepositoryInterface
{
    public interface ITagRepository:IRepository<Tag>
    {
        List<Tag> SearchByKeyWord(string key);
        List<Tag> PageList<s>(int pageIndex, int pageSize, Expression<Func<Tag, s>> orderByLambda);
        Tag GetTagByTagName(string tagName);
        List<Tag> GetTopTags(int count);
    }
}

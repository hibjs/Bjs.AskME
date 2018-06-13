using AskME.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AskME.Core.ServiceInterface
{
    public interface ITagService
    {
        List<Tag> PageList(int pageIndex, int pageSize, out int count);
        bool Add(Tag topic);
        bool Modify(Tag topic);
        Tag GetTopic(int id);
        List<Tag> SearchByTag(string key);
        List<Tag> PageList<s>(int pageIndex, int pageSize, Expression<Func<Tag, s>> orderByLambda);
        Tag GetTagByTagName(string tagName);
        List<Tag> GetTopTags(int count);
    }
}

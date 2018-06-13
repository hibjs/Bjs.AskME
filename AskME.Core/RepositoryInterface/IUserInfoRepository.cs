using AskME.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AskME.Core.RepositoryInterface
{
    public interface IUserInfoRepository : IRepository<UserInfo>
    {
        bool AddFollowTag(int userId, int tagId);
        List<UserInfo> PageList<s>(int pageIndex, int pageSize, Expression<Func<UserInfo, s>> orderByLambda);
        bool HasFollowed(string tagName, int userId);

        bool AddTag(Tag tag);
        List<Tag> GetUserFollowedTags(int id);
    }
}

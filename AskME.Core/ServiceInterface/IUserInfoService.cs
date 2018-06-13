using AskME.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AskME.Core.ServiceInterface
{
    public interface IUserInfoService
    {
        bool AddUserInfo(string userName, string email, string password, out string msg);
        UserInfo UserIsExists(string userName, string password);
        bool IsEmailExists(string email);
        UserInfo GetUser(int id);
        bool Edit(UserInfo userInfo);
        bool AddFollowTag(int userId, int tagId);
        List<UserInfo> PageList<s>(int pageIndex, int pageSize, Expression<Func<UserInfo, s>> orderByLambda);

        bool HasFollowed(string tagName, int userId);
        bool AddTag(Tag tag);

        List<Tag> GetUserFollowedTags(int id);
    }
}

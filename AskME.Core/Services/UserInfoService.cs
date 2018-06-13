using AskME.Core.Entities;
using AskME.Core.Interfaces;
using AskME.Core.RepositoryInterface;
using AskME.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AskME.Core.Services
{
    public class UserInfoService : IUserInfoService
    {
        private IUserInfoRepository _userInfoRepository;

        private IUUID _uUID;
        public UserInfoService(IUserInfoRepository repository, IUUID uUID)
        {
            _userInfoRepository = repository;
            _uUID = uUID;
        }

        ///// <summary>
        ///// 新增用户
        ///// </summary>
        ///// <param name="userInfo"></param>
        ///// <returns></returns>
        public bool AddUserInfo(string userName, string email, string password, out string msg)
        {
            bool success = false;
            UserInfo user = new UserInfo() { UserName = userName, Email = email, Password = password, UId = _uUID.CreateUUID() };
            if (_userInfoRepository.Add(user))
            {
                // TODO 发送邮件激活链接
                success = true;
                msg = "Success";
            }
            else
            {
                msg = "新增失败";
            }
            return success;
        }

        /// <summary>
        /// 用户是否存在
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public UserInfo UserIsExists(string email, string password)
        {
            return _userInfoRepository.List(u => u.Email == email && u.Password == password).FirstOrDefault();
        }

        /// <summary>
        /// 检查邮箱是存在，存在True，不存在False
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public bool IsEmailExists(string email)
        {
            if (_userInfoRepository.List(u => u.Email == email).FirstOrDefault() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool Edit(UserInfo userInfo) => _userInfoRepository.Update(userInfo);
        public UserInfo GetUser(int id) => _userInfoRepository.GetById(id);

        public bool AddFollowTag(int userId, int tagId) => _userInfoRepository.AddFollowTag(userId, tagId);


        public List<UserInfo> PageList<s>(int pageIndex, int pageSize, Expression<Func<UserInfo, s>> orderByLambda) => _userInfoRepository.PageList<s>(pageIndex, pageSize, orderByLambda);

        public bool HasFollowed(string tagName, int userId) => _userInfoRepository.HasFollowed(tagName, userId);

        public bool AddTag(Tag tag) => _userInfoRepository.AddTag(tag);

        public List<Tag> GetUserFollowedTags(int id) => _userInfoRepository.GetUserFollowedTags(id);
    }
}

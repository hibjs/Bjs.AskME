using AskME.Core.Entities;
using AskME.Core.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AskME.Infrastructure.Data
{
    public class UserInfoRepository : Repository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(AskMEDbContext dbContext) : base(dbContext) { }



        public bool AddFollowTag(int userId, int tagId)
        {
            if (_dbContext.TagFollowers.Where(tf => tf.UserInfoId == userId && tf.TagId == tagId).FirstOrDefault() == null)
            {
                TagFollower tagFollower = new TagFollower { UserInfoId = userId, TagId = tagId };
                _dbContext.TagFollowers.Add(tagFollower);
                return _dbContext.SaveChanges() > 0;
            }
            else
            {
                return true;
            }
        }

        public List<UserInfo> PageList<s>(int pageIndex, int pageSize, Expression<Func<UserInfo, s>> orderByLambda)
        {
            List<UserInfo> users = _dbContext.UserInfos.Take(pageSize).Skip((pageIndex - 1) * pageSize).OrderByDescending(orderByLambda).ToList();
            foreach (UserInfo user in users)
            {
                List<TagFollower> tagFollowers = _dbContext.TagFollowers.Where(tf => tf.UserInfoId == user.Id).ToList();
                List<Tag> tags = new List<Tag>();
                foreach (TagFollower tagFollower in tagFollowers)
                {
                    tags.Add(_dbContext.Tags.Where(t => t.Id == tagFollower.TagId).FirstOrDefault());
                }
                user.Tags = tags.Take(3).ToList();
            }
            return users;
        }



        public bool HasFollowed(string tagName, int userId)
        {
            Tag tag = _dbContext.Tags.Where(t => t.TagName == tagName).FirstOrDefault();
            List<TagFollower> tagFollower = _dbContext.TagFollowers.Where(tf => tf.UserInfoId == userId).ToList();
            foreach (TagFollower item in tagFollower)
            {
                if (item.TagId == tag.Id)
                {
                    return true;
                }
            }
            return false;
        }


        public List<Tag> GetUserFollowedTags(int id)
        {
            List<TagFollower> tagFollowers = _dbContext.TagFollowers.Where(tf => tf.UserInfoId == id).ToList();
            List<Tag> tags = new List<Tag>();
            foreach (TagFollower item in tagFollowers)
            {
                tags.Add(_dbContext.Tags.Where(t => t.Id == item.TagId).FirstOrDefault());
            }
            return tags;
        }

        /// <summary>
        /// 保存标签
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool AddTag(Tag tag)
        {
            _dbContext.Tags.Add(tag);
            return _dbContext.SaveChanges() > 0;
        }
    }
}

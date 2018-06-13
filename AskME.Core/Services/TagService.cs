using AskME.Core.Entities;
using AskME.Core.RepositoryInterface;
using AskME.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AskME.Core.Services
{
    public class TagService : ITagService
    {
        public ITagRepository _tagRepository;
        public TagService(ITagRepository repository)
        {
            _tagRepository = repository;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Tag> PageList(int pageIndex, int pageSize, out int count)
        {
            return _tagRepository.LoadPageEntities(pageIndex, pageSize, out count, t => t.Disabled == Entities.Enum.Status.OK, t => true, true).ToList();
        }


        public bool Add(Tag topic) => _tagRepository.Add(topic);

        public bool Modify(Tag topic) => _tagRepository.Update(topic);

        public Tag GetTopic(int id) => _tagRepository.GetById(id);

        public List<Tag> SearchByTag(string key) => _tagRepository.SearchByKeyWord(key);

        public List<Tag> PageList<s>(int pageIndex, int pageSize, Expression<Func<Tag, s>> orderByLambda) => _tagRepository.PageList(pageIndex, pageSize, orderByLambda);

        public Tag GetTagByTagName(string tagName) => _tagRepository.GetTagByTagName(tagName);

        public List<Tag> GetTopTags(int count) => _tagRepository.GetTopTags(count);

    }
}

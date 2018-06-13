using AskME.Core.Entities;
using AskME.Core.RepositoryInterface;
using AskME.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AskME.Core.Services
{
    public class QuestionService : IQuestionService
    {
        private IQuestionRepository _questionRepository;
        public QuestionService(IQuestionRepository repository)
        {
            _questionRepository = repository;
        }





        public bool Add(Question question, string[] ids) => _questionRepository.Add(question, ids);


        public List<Question> PageList<s>(int pageIndex, int pageSize, Expression<Func<Question, s>> orderByLambda) =>
            _questionRepository.PageList(pageIndex, pageSize, orderByLambda);

        public int Count() => _questionRepository.List().Count();

        public Question GetQuestion(int id) => _questionRepository.GetQuestion(id);

        public List<Question> GetQuestionsByUserId(int id) => _questionRepository.GetQuestionsByUserId(id);

        public List<Question> GetAnswersByUserId(int id) => _questionRepository.GetAnswersByUserId(id);

        public List<Question> PageListByTagged<s>(int pageIndex, int pageSize, Expression<Func<Question, s>> orderByLambda, string tagName) => _questionRepository.PageListByTagged<s>(pageIndex, pageSize, orderByLambda, tagName);

    }
}

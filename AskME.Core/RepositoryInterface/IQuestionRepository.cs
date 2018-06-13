using AskME.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AskME.Core.RepositoryInterface
{
    public interface IQuestionRepository : IRepository<Question>
    {
        bool Add(Question question, string[] ids);

        List<Question> PageList<s>(int pageIndex, int pageSize, Expression<Func<Question, s>> orderByLambda);

        Question GetQuestion(int id);

        List<Question> GetQuestionsByUserId(int id);

        List<Question> GetAnswersByUserId(int id);

        List<Question> PageListByTagged<s>(int pageIndex, int pageSize, Expression<Func<Question, s>> orderByLambda, string tagName);
    }
}

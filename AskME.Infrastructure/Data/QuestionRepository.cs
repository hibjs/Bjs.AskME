using AskME.Core.Entities;
using AskME.Core.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AskME.Infrastructure.Data
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(AskMEDbContext dbContext) : base(dbContext) { }



        /// <summary>
        /// 分页查询Question
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByLambda"></param>
        /// <returns></returns>
        public List<Question> PageList<s>(int pageIndex, int pageSize, Expression<Func<Question, s>> orderByLambda)
        {
            List<Question> questions = _dbContext.Questions.Take(pageSize).Skip((pageIndex - 1) * pageSize).OrderByDescending(orderByLambda).ToList();
            foreach (Question q in questions)
            {
                List<QuestionTags> questionTags = _dbContext.QuestionTags.Where(qt => qt.QuestionId == q.Id).ToList();
                List<Tag> tags = new List<Tag>();
                foreach (QuestionTags qt in questionTags)
                {
                    tags.Add(_dbContext.Tags.Where(t => t.Id == qt.TagId).FirstOrDefault());
                }
                q.Tags = tags;
                q.Count = _dbContext.Answers.Where(a => a.QuestionId == q.Id).Count();
                q.UserName = _dbContext.UserInfos.Where(u => u.Id == q.UserInfoId).FirstOrDefault().UserName;
            }
            return questions;
        }

        /// <summary>
        /// 添加问题
        /// </summary>
        /// <param name="question"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Add(Question question, string[] ids)
        {
            _dbContext.Questions.Add(question);
            List<QuestionTags> questionTags = new List<QuestionTags>();
            foreach (string item in ids)
            {
                questionTags.Add(new QuestionTags { QuestionId = question.Id, TagId = Convert.ToInt32(item) });
            }
            _dbContext.QuestionTags.AddRange(questionTags);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 查询问题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Question GetQuestion(int id)
        {
            Question question = _dbContext.Questions.Find(id);
            question.UserName = _dbContext.UserInfos.Find(question.UserInfoId).UserName;
            List<QuestionTags> questionTags = _dbContext.QuestionTags.Where(qt => qt.QuestionId == question.Id).ToList();
            List<Tag> tags = new List<Tag>();
            foreach (QuestionTags qt in questionTags)
            {
                tags.Add(_dbContext.Tags.Find(qt.TagId));
            }
            question.Tags = tags;
            List<Answer> answers = _dbContext.Answers.Where(a => a.QuestionId == id).ToList();
            foreach (Answer answer in answers)
            {
                answer.UserName = _dbContext.UserInfos.Where(u => u.Id == answer.UserId).FirstOrDefault().UserName;
            }
            question.Answers = answers;
            question.Count = answers.Count;
            return question;
        }


        /// <summary>
        /// 查询用户的所有提问
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Question> GetQuestionsByUserId(int id)
        {
            List<Question> questions = _dbContext.Questions.Where(q => q.UserInfoId == id).ToList();
            foreach (Question q in questions)
            {
                List<QuestionTags> questionTags = _dbContext.QuestionTags.Where(qt => qt.QuestionId == q.Id).ToList();
                List<Tag> tags = new List<Tag>();
                foreach (QuestionTags qt in questionTags)
                {
                    tags.Add(_dbContext.Tags.Where(t => t.Id == qt.TagId).FirstOrDefault());
                }
                q.Tags = tags;
                q.UserName = _dbContext.UserInfos.Where(u => u.Id == q.UserInfoId).FirstOrDefault().UserName;
                q.Count = _dbContext.Answers.Where(a => a.QuestionId == q.Id).ToList().Count;
            }
            return questions;
        }

        /// <summary>
        /// 查询用户的所有回答
        /// </summary>
        /// <returns></returns>
        public List<Question> GetAnswersByUserId(int id)
        {
            List<Answer> answers = _dbContext.Answers.Where(a => a.UserId == id).ToList();

            List<Question> questions = new List<Question>();
            foreach (Answer answer in answers)
            {
                Question question = new Question();
                question = _dbContext.Questions.Where(q => q.Id == answer.QuestionId).FirstOrDefault();
                question.AnswerContent = answer.AnserContent;
                question.AnswerId = answer.Id;
                questions.Add(question);
            }
            return questions;
        }



        /// <summary>
        /// 分页查询具有Tag的Question
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByLambda"></param>
        /// <returns></returns>
        public List<Question> PageListByTagged<s>(int pageIndex, int pageSize, Expression<Func<Question, s>> orderByLambda, string tagName)
        {
            Tag tag = _dbContext.Tags.Where(t => t.TagName == tagName).FirstOrDefault();
            List<QuestionTags> questionTags = _dbContext.QuestionTags.Where(qt => qt.TagId == tag.Id).ToList();
            List<Question> questions = new List<Question>();
            foreach (QuestionTags questionTag in questionTags)
            {
                questions.Add(_dbContext.Questions.Where(q => q.Id == questionTag.QuestionId).FirstOrDefault());
            }
            // 去重？？
            questions = questions.Distinct().ToList();
            foreach (Question q in questions)
            {
                List<QuestionTags> inerQuestionTags = _dbContext.QuestionTags.Where(qt => qt.QuestionId == q.Id).ToList();
                List<Tag> tags = new List<Tag>();
                foreach (QuestionTags qt in inerQuestionTags)
                {
                    tags.Add(_dbContext.Tags.Where(t => t.Id == qt.TagId).FirstOrDefault());
                }
                q.Tags = tags;
                q.Count = _dbContext.Answers.Where(a => a.QuestionId == q.Id).Count();
                q.UserName = _dbContext.UserInfos.Where(u => u.Id == q.UserInfoId).FirstOrDefault().UserName;
            }
            return questions;
        }
    }
}

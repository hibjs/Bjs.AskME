/**
 *
 * Copyright,2018,b13
 * FileName:IAsyncRepository
 * 异步操作仓储接口
 *
 * @auhor:b13
 * @create:2018/1/9 9:21:31
 * @version:V1.0.0.0
 *
 *
 **/
using AskME.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AskME.Core.RepositoryInterface
{
    public interface IAsyncRepository<T> where T:BaseEntity
    {
        //Task<T> GetByIdAsync(int id);
        //Task<List<T>> ListAllAsync();
        //Task<T> AddAsync(T entity);
        //Task UpdateAsync(T entity);
        //Task DeleteAsync(T entity);
    }
}

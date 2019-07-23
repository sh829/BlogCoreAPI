using BlogCore.IRepository.Base;
using BlogCore.Model;
using BlogCore.Repository.Sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Repository.Base
{
    public class BaseRespository<TEntity> : IBaseRespository<TEntity> where TEntity:class ,new ()
    {
        public DbContext context { get; set; }
        internal SqlSugarClient db { get; set; }
        internal SimpleClient<TEntity> entityDb { get; set; }
        public BaseRespository()
        {
            DbContext.Init(BaseDBConfig.ConnectionString, (DbType)BaseDBConfig.DbType);
            context = DbContext.GetDbContext();
            db = context.Db;
            entityDb = context.GetEntityDB<TEntity>(db);
        }

        public async Task<TEntity> QueryById(object id)
        {
            return await db.Queryable<TEntity>().In(id).SingleAsync();
        }

        public async Task<TEntity> QueryById(int id, bool blnUseCashe = true)
        {
            return await db.Queryable<TEntity>().WithCacheIF(blnUseCashe).In(id).SingleAsync();
        }

        public async Task<List<TEntity>> QueryByIds(object[] lstIds)
        {
            return await db.Queryable<TEntity>().In(lstIds).ToListAsync();
        }

        public async Task<int> Add(TEntity model)
        {
           var insert= db.Insertable(model);
            return await insert.ExecuteReturnIdentityAsync();
        }
        /// <summary>
        /// 写入实体数据，指定插入的列
        /// </summary>
        /// <param name="model">实体数据</param>
        /// <param name="insertColums">指定只插入的列</param>
        /// <returns>返回自增量列</returns>
        public async Task<int> Add(TEntity model,Expression<Func<TEntity,object>> insertColums=null)
        {
            var insert = db.Insertable(model);
            if (insertColums == null)
            {
                return await insert.ExecuteReturnIdentityAsync();
            }
            return await insert.InsertColumns(insertColums).ExecuteReturnIdentityAsync();
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="listEnties"></param>
        /// <returns></returns>
        public async Task<int> Add(List<TEntity> listEnties)
        {
            return await db.Insertable(listEnties.ToArray()).ExecuteCommandAsync();
        }
        public async Task<bool> DeleteById(object id)
        {
            return await db.Deleteable<TEntity>(id).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> Delete(TEntity model)
        {
            return await db.Deleteable(model).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> DeleteByIds(object[] objs)
        {
            return await db.Deleteable<TEntity>().In(objs).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> Update(TEntity model)
        {
            return await db.Updateable(model).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> Update(TEntity model, string strWhere)
        {
            return await db.Updateable(model).Where(strWhere).ExecuteCommandHasChangeAsync();
        }

        public async Task<List<TEntity>> Query()
        {
            return await db.Queryable<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToListAsync();
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await db.Queryable<TEntity>().WhereIF(whereExpression!=null,whereExpression).OrderByIF(!string.IsNullOrEmpty(strOrderByFileds),strOrderByFileds).ToListAsync();
         }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).ToListAsync();
        }

        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToListAsync();
        }
        /// <summary>
        /// 查询当前N条数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return await db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).Take(intTop).ToListAsync();
        }
        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="intTop"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">当前页码</param>
        /// <param name="intPageSize">每页大小</param>
        /// <param name="strOrderByFileds">排序字段</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return await db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToPageListAsync(intPageIndex, intPageSize);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return await db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToPageListAsync(intPageIndex, intPageSize);

        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">每页大小</param>
        /// <param name="strOrderByFileds">排序字段</param>
        /// <returns></returns>
        public async Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null)
        {
            RefAsync<int> totalCount = 0;
            var list= await db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToPageListAsync(intPageIndex, intPageSize,totalCount);
            int pageCount = Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal()).ObjToInt();
            return new PageModel<TEntity>() { dataCount = totalCount, pageCount = pageCount, page = intPageIndex, pageSize = intPageSize, data = list };
        }
    }
}

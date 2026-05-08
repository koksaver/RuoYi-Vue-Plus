using SqlSugar;
using RuoYi.Common.Core;

namespace RuoYi.Common.Mybatis
{
    public class BaseRepository<T> : SimpleClient<T> where T : BaseEntity, new()
    {
        public BaseRepository(ISqlSugarClient context) : base(context)
        {
        }

        public ISugarQueryable<T> GetPageableQuery(ISugarQueryable<T> query, BaseEntity entity)
        {
            if (!string.IsNullOrEmpty(entity.OrderByColumn))
            {
                var orderByType = entity.IsAsc == "asc" ? OrderByType.Asc : OrderByType.Desc;
                query = query.OrderBy(entity.OrderByColumn, orderByType);
            }

            return query;
        }

        public async Task<PageResult<T>> GetPageListAsync(ISugarQueryable<T> query, int pageNum, int pageSize)
        {
            var total = await query.Clone().CountAsync();
            var rows = await query.Clone().Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PageResult<T>(rows, total);
        }
    }
}
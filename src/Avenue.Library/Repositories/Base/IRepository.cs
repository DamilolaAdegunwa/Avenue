namespace Avenue.Library.Repositories.Base
{
	public interface IRepository<T> where T : class
	{
		BloggingContext GetDbContext();
		DbSet<T> GetDbSet();
		Task<T> Get(int id);
		Task<IEnumerable<T>> Get();
		Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
		Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
		Task<T> Create(T entity);
		Task AddRangeAsync(IEnumerable<T> entities);
		Task<bool> Delete(T entity);
		Task RemoveRangeAsync(IEnumerable<T> entities);
		Task<T> Update(T entity);
		Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int page = 1, int pageSize = 10, string searchKeyword = null);
		IQueryable<T> AsNoTracking();
		Task<List<T>> QueryAsync(int page = 1, int pageSize = 10, string searchKeyword = null);
		Task<List<T>> Get(string propertyName, string value);
	}
}

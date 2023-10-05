namespace Avenue.Library.Repositories.Base
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly BloggingContext Context;
		protected readonly DbSet<T> DbSet;
		public Repository(BloggingContext context)
		{
			Context = context;
			DbSet = context.Set<T>();
		}
		public virtual BloggingContext GetDbContext()
		{
			return Context;
		}
		public virtual DbSet<T> GetDbSet()
		{
			return DbSet;
		}

		public virtual async Task<T> Get(int id)
		{
			return await DbSet.FindAsync(id);
		}

		public virtual async Task<IEnumerable<T>> Get()
		{
			return await DbSet.AsNoTracking().ToListAsync();
		}

		public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
		{
			return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
		}

		public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
		{
			return await DbSet.AsNoTracking().SingleOrDefaultAsync(predicate);
		}

		public virtual async Task<T> Create(T entity)
		{
			var response = await DbSet.AddAsync(entity);
			var changes = Context.SaveChanges();
			return response.Entity;
		}

		public virtual async Task AddRangeAsync(IEnumerable<T> entities)
		{
			await DbSet.AddRangeAsync(entities);
		}

		public virtual async Task<bool> Delete(T entity)
		{
			var response = DbSet.Remove(entity);
			var change = response.State == EntityState.Deleted;
			//return change;
			Context.SaveChanges();
			return change;
		}

		public virtual async Task RemoveRangeAsync(IEnumerable<T> entities)
		{
			DbSet.RemoveRange(entities);
		}

		public virtual async Task<T> Update(T entity)
		{
			var res = DbSet.Update(entity);
			var change = res.State == EntityState.Modified;
			//return change;
			Context.SaveChanges();
			return res.Entity;
		}

		public async Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int page = 1, int pageSize = 10, string searchKeyword = null)
		{
			IQueryable<T> query = DbSet.AsNoTracking(); // Use AsNoTracking for read-only select

			if (!string.IsNullOrWhiteSpace(searchKeyword))
			{
				query = ApplySearch(query, searchKeyword);
			}

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			if (include != null)
			{
				query = include(query);
			}

			if (page < 1)
			{
				page = 1;
			}

			if (pageSize < 1)
			{
				pageSize = 10; // Default page size
			}

			int skip = (page - 1) * pageSize;

			return query.Skip(skip).Take(pageSize);
		}

		public async Task<List<T>> QueryAsync(int page = 1, int pageSize = 10, string searchKeyword = null)
		{
			IQueryable<T> query = DbSet.AsNoTracking(); // Use AsNoTracking for read-only select

			if (!string.IsNullOrWhiteSpace(searchKeyword))
			{
				query = ApplySearch(query, searchKeyword);
			}

			if (page < 1)
			{
				page = 1;
			}

			if (pageSize < 1)
			{
				pageSize = 10; // Default page size
			}

			int skip = (page - 1) * pageSize;

			return query.Skip(skip).Take(pageSize).ToList();
		}

		public IQueryable<T> AsNoTracking()
		{
			return DbSet.AsNoTracking();
		}

		private IQueryable<T> ApplySearch(IQueryable<T> query, string searchKeyword)
		{
			try
			{
				var searchableProperties = typeof(T).GetProperties().Where(prop => prop.PropertyType == typeof(string)).ToArray();

				if (searchableProperties.Length == 0)
				{
					// No searchable string properties found, return the original query
					return query;
				}

				Expression<Func<T, bool>> searchPredicate = null;

				foreach (var prop in searchableProperties)
				{
					var parameter = Expression.Parameter(typeof(T), "x");
					var propertyExpression = Expression.Property(parameter, prop);
					var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
					var containsExpression = Expression.Call(propertyExpression, containsMethod, Expression.Constant(searchKeyword));

					//var containsExpression = Expression.Call(propertyExpression, typeof(string).GetMethod("Contains"), Expression.Constant(searchKeyword));//this line

					var orPredicate = Expression.Lambda<Func<T, bool>>(
						containsExpression,
						parameter
					);

					searchPredicate = searchPredicate == null
						? orPredicate
						: searchPredicate.Or(orPredicate.Expand()); // Use LINQKit's Or extension method
				}

				if (searchPredicate != null)
				{
					query = query.AsExpandable()
						.Where(searchPredicate);
				}

				return query;
			}
			catch (Exception ex)
			{

				throw;
			}

		}

		public async Task<List<T>> Get(string propertyName, string value)
		{
			// Find the property on T with the provided name
			PropertyInfo property = typeof(T).GetProperty(propertyName);

			// Return null if the property doesn't exist or is not of type string
			if (property == null || property.PropertyType != typeof(string))
				return null;

			// Build the equivalent of x => x.Property == value
			var parameter = Expression.Parameter(typeof(T), "x");
			var propertyExpression = Expression.Property(parameter, property);
			var constantExpression = Expression.Constant(value);
			var equalityExpression = Expression.Equal(propertyExpression, constantExpression);
			var lambdaExpression = Expression.Lambda<Func<T, bool>>(equalityExpression, parameter);

			// Execute the query and return the results
			return await Context.Set<T>().Where(lambdaExpression).ToListAsync();
		}
	}
}
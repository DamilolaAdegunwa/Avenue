//using Avenue.Library.Entities;
//using static System.Net.Mime.MediaTypeNames;

//namespace Avenue.Library.Repositories.Implementations
//{
//	public class BlogRepository: Repository<Blog>, IBlogRepository
//	{
//		private int _blogNumber;
//		private bool _addWhereClause = true;
//		private readonly BloggingContext _context;
//		public BlogRepository(BloggingContext context): base(context)
//		{
//			_context = context;
//		}

//		public Task<int> Create(Blog request)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<bool> Delete(int id)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<IQueryable<Blog>> Get()
//		{
//			throw new NotImplementedException();
//		}

//		public Task<Blog> Get(int id)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<bool> Update(Blog request)
//		{
//			throw new NotImplementedException();
//		}
//		//sample methods
//		public async Task Test1()
//		{
//			using(var db = _context)
//			{
//				var blogs = db.Blogs .Where(b => b.Rating > 3)
//				.OrderBy(b => b.Url)
//				.ToList();
//			}

//			using (var db = _context)
//			{
//				var blog = new Blog { Url = "http://sample.com" };
//				db.Blogs.Add(blog);
//				db.SaveChanges();
//			}
//		}
//		public async Task Test2(int i = 0)
//		{
//			var context = _context;
//			switch (i)
//			{
//				case 0:
					
//					await context.Tags.ExecuteDeleteAsync();
//					break;

//				case 1:
//					await context.Tags.Where(t => t.Text.Contains(".NET")).ExecuteDeleteAsync();
//					break;

//				case 2:
//					var b = context.Database.ExecuteSqlRaw("DELETE FROM [t] FROM[Tags] AS[t] WHERE[t].[Text] LIKE N'%.NET%'");
//					break;
//				case 3:
//					var c = await context.Tags.Where(t => t.Posts.All(e => e.PublishedOn.Year < 2022)).ExecuteDeleteAsync();
//					var d = await context.Blogs.ExecuteUpdateAsync(s => s.SetProperty(b => b.Name, b => b.Name + " *Featured!*"));
//					var e = await context.Posts
//					.Where(p => p.PublishedOn.Year < 2022)
//					.ExecuteUpdateAsync(s => s
//					.SetProperty(b => b.Title, b => b.Title + " (" + b.PublishedOn.Year + ")")
//					.SetProperty(b => b.Content, b => b.Content + " ( This content was published in " + b.PublishedOn.Year
//					+ ")"));
//					await context.AddAsync( new Blog { Name = "MyBlog"});
//					await context.SaveChangesAsync();
//					break;
//				default: throw new Exception("invalid value for 'i'");
//			}
//		}
//		public int ExpressionApiWithConstant()
//		{
//			var url = "blog" + Interlocked.Increment(ref _blogNumber);
//			using var context = _context;

//			IQueryable<Blog> query = context.Blogs;

//			if (_addWhereClause)
//			{
//				var blogParam = Expression.Parameter(typeof(Blog), "b");
//				var whereLambda = Expression.Lambda<Func<Blog, bool>>(
//					Expression.Equal(
//						Expression.MakeMemberAccess(
//							blogParam,
//							typeof(Blog).GetMember(nameof(Blog.Url)).Single()),
//						Expression.Constant(url)),
//					blogParam);

//				query = query.Where(whereLambda);
//			}

//			return query.Count();
//		}

//		public void Test3()
//		{
//			using var context = _context;
//			IQueryable<Blog> query = context.Blogs;

//			query = query.Where(b => b.Url != null);

//		}

//		public void Test3b()
//		{
//			using var context = _context;
//			IQueryable<Blog> query = context.Blogs;

//			// Define a parameter for the Blog object
//			var blogParam = Expression.Parameter(typeof(Blog), "b");

//			// Create an expression for accessing the Url property of the Blog
//			var urlPropertyAccess = Expression.MakeMemberAccess(
//				blogParam,
//				typeof(Blog).GetMember(nameof(Blog.Url)).Single());

//			// Create an expression for checking if the Url is not null
//			var nonNullCheck = Expression.NotEqual(urlPropertyAccess, Expression.Constant(null, typeof(string)));

//			// Wrap the non-null check into a lambda expression
//			var whereLambda = Expression.Lambda<Func<Blog, bool>>(nonNullCheck, blogParam);

//			// Apply the filtering condition using the Where extension method
//			query = query.Where(whereLambda);
//		}

//		public void Test4()
//		{
//			using var context = _context;
//			IQueryable<Blog> query = context.Blogs;

//			query = query.Where(b => b.Url != null && b.Name.Length > 5);

//		}
//		public void Test4b()
//		{
//			var _a_b = typeof(Blog).GetMember(nameof(Blog.Url)).FirstOrDefault().Name;

//			using var context = _context;
//			IQueryable<Blog> query = context.Blogs;

//			// Define a parameter for the Blog object
//			var blogParam = Expression.Parameter(typeof(Blog), "b");

//			// Create an expression for accessing the Url property of the Blog
//			var urlPropertyAccess = Expression.MakeMemberAccess(
//				blogParam,
//				typeof(Blog).GetMember(nameof(Blog.Url)).Single());

//			// Create an expression for accessing the Name property of the Blog
//			var namePropertyAccess = Expression.MakeMemberAccess(
//				blogParam,
//				typeof(Blog).GetMember(nameof(Blog.Name)).Single());

//			// Create an expression for getting the Length of the Name property
//			var nameLength = Expression.Property(namePropertyAccess, "Length");

//			// Create an expression for checking if the Url is not null
//			var nonNullCheck = Expression.NotEqual(urlPropertyAccess, Expression.Constant(null, typeof(string)));

//			// Create an expression for checking if the Name's length is greater than 5
//			var lengthCheck = Expression.GreaterThan(nameLength, Expression.Constant(5));

//			// Combine the two conditions using the 'AND' operator
//			var combinedCondition = Expression.AndAlso(nonNullCheck, lengthCheck);

//			// Wrap the combined condition into a lambda expression
//			var whereLambda = Expression.Lambda<Func<Blog, bool>>(combinedCondition, blogParam);

//			// Apply the filtering condition using the Where extension method
//			query = query.Where(whereLambda);
//		}

//	}
//}
using Avenue.Library.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace Avenue.Library.Repositories.Implementations
{
	public class BlogRepository: Repository<Blog>, IBlogRepository
	{
		private readonly BloggingContext _context;
		public BlogRepository(BloggingContext context): base(context)
		{
			_context = context;
		}

		public Task<int> Create(Blog request)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IQueryable<Blog>> Get()
		{
			throw new NotImplementedException();
		}

		public Task<Blog> Get(int id)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Update(Blog request)
		{
			throw new NotImplementedException();
		}
		//sample methods
		public async Task Test1()
		{
			using(var db = new BloggingContext())
			{
				var blogs = db.Blogs .Where(b => b.Rating > 3)
				.OrderBy(b => b.Url)
				.ToList();
			}

			using (var db = new BloggingContext())
			{
				var blog = new Blog { Url = "http://sample.com" };
				db.Blogs.Add(blog);
				db.SaveChanges();
			}
		}
		public async Task Test2(int i = 0)
		{
			var context = new BloggingContext();
			switch (i)
			{
				case 0:
					
					await context.Tags.ExecuteDeleteAsync();
					break;

				case 1:
					await context.Tags.Where(t => t.Text.Contains(".NET")).ExecuteDeleteAsync();
					break;

				case 2:
					var b = context.Database.ExecuteSqlRaw("DELETE FROM [t] FROM[Tags] AS[t] WHERE[t].[Text] LIKE N'%.NET%'");
					break;
				case 3:
					var c = await context.Tags.Where(t => t.Posts.All(e => e.PublishedOn.Year < 2022)).ExecuteDeleteAsync();
					var d = await context.Blogs.ExecuteUpdateAsync(s => s.SetProperty(b => b.Name, b => b.Name + " *Featured!*"));
					var e = await context.Posts
					.Where(p => p.PublishedOn.Year < 2022)
					.ExecuteUpdateAsync(s => s
					.SetProperty(b => b.Title, b => b.Title + " (" + b.PublishedOn.Year + ")")
					.SetProperty(b => b.Content, b => b.Content + " ( This content was published in " + b.PublishedOn.Year
					+ ")"));
					await context.AddAsync( new Blog { Name = "MyBlog"});
					await context.SaveChangesAsync();
					break;
				default: throw new Exception("invalid value for 'i'");
			}
			
		}
		public class TagDto : Tag
		{
			public TagDto(string id, string text):base(id,text) 
			{
				Id = id;
				Text = text;
			}
		}
	}
}
namespace Avenue.Library.Persistence
{
	public class BloggingContext : DbContext
	{
		public BloggingContext() { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Author>().OwnsOne(
				author => author.Contact, ownedNavigationBuilder =>
				{
					ownedNavigationBuilder.OwnsOne(contactDetails => contactDetails.Address);
				});
		}
	}
}
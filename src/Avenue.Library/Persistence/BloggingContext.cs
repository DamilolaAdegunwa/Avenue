namespace Avenue.Library.Persistence
{
	public class BloggingContext : DbContext
	{
		public BloggingContext()
		{

		}
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Tag> Tags { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			//1a) this create coulmns of each of the object's property
			modelBuilder.Entity<Author>().OwnsOne(
				author => author.Contact, ownedNavigationBuilder =>
				{
					ownedNavigationBuilder.OwnsOne(contactDetails => contactDetails.Address);
				});

			//1b) if you'd rather have the navigation properties as separate tables
			//	modelBuilder.Entity<Author>().OwnsOne(
			//author => author.Contact, ownedNavigationBuilder =>
			//{
			//	ownedNavigationBuilder.ToTable("Contacts");
			//	ownedNavigationBuilder.OwnsOne(
			//		contactDetails => contactDetails.Address, ownedOwnedNavigationBuilder =>
			//		{
			//			ownedOwnedNavigationBuilder.ToTable("Addresses");
			//		});
			//});

			//1c) have the nav obj as json
			//modelBuilder.Entity<Author>().OwnsOne(
			//	author => author.Contact, ownedNavigationBuilder =>
			//	{
			//		ownedNavigationBuilder.ToJson();
			//		ownedNavigationBuilder.OwnsOne(contactDetails => contactDetails.Address);
			//	});

			//2) 
			modelBuilder.Entity<Post>().OwnsOne(
				post => post.Metadata, ownedNavigationBuilder =>
				{
					ownedNavigationBuilder
						.ToJson();

					ownedNavigationBuilder
						.OwnsMany(metadata => metadata.TopSearches);

					ownedNavigationBuilder
						.OwnsMany(metadata => metadata.TopGeographies);

					ownedNavigationBuilder
						.OwnsMany(metadata => metadata.Updates, ownedOwnedNavigationBuilder =>
						{
							ownedOwnedNavigationBuilder
								.OwnsMany(update => update.Commits);
						});
				});

			//3)
			modelBuilder.Entity<Author>()
			.Property(u => u.PhoneNumbers)
			.HasConversion(
				v => JsonConvert.SerializeObject(v), // Convert List<string> to JSON string
				v => JsonConvert.DeserializeObject<List<string>>(v) // Convert JSON string back to List<string>
			);

			////4)
			//modelBuilder.Entity<User>()
			//  .OwnsOne(
			//   p => p.Address,
			//   addressBuilder =>
			//   {
			//	   addressBuilder.Property(a => a.Street);
			//	   addressBuilder.Property(a => a.City);
			//	   addressBuilder.Property(a => a.Postalcode);
			//	   addressBuilder.Property(a => a.Country);
			//   }
			//  )
			//  .HasConversion(
			//   v => JsonConvert.SerializeObject(v), // Convert Address to JSON string
			//   v => JsonConvert.DeserializeObject<Address>(v) // Convert JSON string back to Address
			//  );

			////4b)
			//modelBuilder.Entity<Person>()
			//.OwnsOne(
			//	p => p.Address,
			//	addressBuilder =>
			//	{
			//		addressBuilder.Property<string>("Street");
			//		addressBuilder.Property(a => a.Street);
			//		addressBuilder.Property(a => a.City);
			//		addressBuilder.Property(a => a.Postalcode);
			//		addressBuilder.Property(a => a.Country);
			//	}
			//)
			//.HasConversion(
			//	v => JsonConvert.SerializeObject(v), // Convert Address to JSON string
			//	v => JsonConvert.DeserializeObject<Address>(v) // Convert JSON string back to Address
			//);

			//5)
			// Create an index on the "age" property within the "Attributes" JSON column
			modelBuilder.Entity<Author>()
				.HasIndex("Attributes->'$.age'")
				.HasDatabaseName("IX_Author_Age");
			modelBuilder.Entity<Blog>().Property(e => e.Id).UseHiLo();
			modelBuilder.Entity<Post>().Property(e => e.Id).UseHiLo();
		}
	}
}
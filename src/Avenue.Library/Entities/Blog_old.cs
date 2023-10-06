//namespace Avenue.Library.Entities
//{
//	[Table("Blogs")]
//	public class Blog
//	{
//		public Blog()
//		{
			
//		}
//		public Blog(string name)
//		{
//			Name = name;
//		}

//		[Key]
//		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//		[JsonProperty("blogId")]
//		[JsonPropertyName("blogId")]
//		public int Id { get; set; }

//		[Required]
//		[MaxLength(100)]
//		[JsonProperty("blogName")]
//		[JsonPropertyName("blogName")]
//		[Column(TypeName = "nvarchar(100)")]
//		public string Name { get; set; }

//		public List<Post> Posts { get; } = new();
//		public string Url { get; set; }
//		public int Rating { get; set; }
//		[Precision(18,2)]
//		public decimal Amount { get; set; }
//	}

//	[Table("Posts")]
//	public class Post
//	{
//		public Post(string title, string content, DateTime publishedOn)
//		{
//			Title = title;
//			Content = content;
//			PublishedOn = publishedOn;
//		}

//		[Key]
//		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//		[JsonProperty("postId")]
//		[JsonPropertyName("postId")]
//		public int Id { get; set; }

//		[Required]
//		[MaxLength(200)]
//		[JsonProperty("postTitle")]
//		[JsonPropertyName("postTitle")]
//		[Column(TypeName = "nvarchar(200)")]
//		public string Title { get; set; }

//		[Required]
//		[MaxLength(5000)]
//		[JsonProperty("postContent")]
//		[JsonPropertyName("postContent")]
//		[Column(TypeName = "nvarchar(max)")]
//		public string Content { get; set; }

//		[Required]
//		[JsonProperty("publishedOn")]
//		[JsonPropertyName("publishedOn")]
//		[Column(TypeName = "datetime2")]
//		public DateTime PublishedOn { get; set; }

//		[ForeignKey("BlogId")]
//		public Blog Blog { get; set; } = null!;
//		public int BlogId { get; set; }

//		public List<Tag> Tags { get; } = new();

//		[ForeignKey("AuthorId")]
//		public Author? Author { get; set; }

//		[ForeignKey("MetadataId")]
//		public PostMetadata? Metadata { get; set; }
//	}

//	public class FeaturedPost : Post
//	{
//		public FeaturedPost(string title, string content, DateTime publishedOn, string promoText)
//			: base(title, content, publishedOn)
//		{
//			PromoText = promoText;
//		}

//		public string PromoText { get; set; }
//	}

//	[Table("Tags")]
//	public class Tag
//	{
//		public Tag(string id, string text)
//		{
//			Id = id;
//			Text = text;
//		}

//		[Key]
//		[MaxLength(50)]
//		[JsonProperty("tagId")]
//		[JsonPropertyName("tagId")]
//		public string Id { get; set; }

//		[Required]
//		[MaxLength(100)]
//		[JsonProperty("tagText")]
//		[JsonPropertyName("tagText")]
//		[Column(TypeName = "nvarchar(100)")]
//		public string Text { get; set; }

//		public List<Post> Posts { get; } = new();
//	}

//	[Table("Authors")]
//	public class Author
//	{
//		public Author(string name)
//		{
//			Name = name;
//			//Attributes = "{}"; // Initialize with an empty JSON object}
//		}

//		[Key]
//		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//		[JsonProperty("authorId")]
//		[JsonPropertyName("authorId")]
//		public int Id { get; set; }

//		[Required]
//		[MaxLength(200)]
//		[JsonProperty("authorName")]
//		[JsonPropertyName("authorName")]
//		[Column(TypeName = "nvarchar(200)")]
//		public string Name { get; set; }

//		[Required]
//		[JsonProperty("contact")]
//		[JsonPropertyName("contact")]
//		public ContactDetails Contact { get; set; } = null!;
//		public int ContactId { get; set; }

//		public List<Post> Posts { get; } = new();

//		[JsonProperty("phoneNumbers")]
//		[JsonPropertyName("phoneNumbers")]
//		public List<string> PhoneNumbers { get; set; }

//		//[JsonProperty("attributes")]
//		//[JsonPropertyName("attributes")]
//		//[Column(TypeName = "nvarchar(max)")]
//		//public string Attributes { get; set; } // JSON column
//	}

//	[Table("ContactDetails")]
//	public class ContactDetails
//	{
//		public int Id { get; set; }
//		[Required]
//		[JsonProperty("address")]
//		[JsonPropertyName("address")]
//		public Address Address { get; set; } = null!;

//		[MaxLength(15)]
//		[JsonProperty("phone")]
//		[JsonPropertyName("phone")]
//		[Column(TypeName = "nvarchar(15)")]
//		public string? Phone { get; set; }


//		public ICollection<Author> Authors { get; set; } = null!;
//	}

//	[Table("Addresses")]
//	public class Address
//	{
//		public Address(string street, string city, string postcode, string country)
//		{
//			Street = street;
//			City = city;
//			Postcode = postcode;
//			Country = country;
//		}

//		[Required]
//		[MaxLength(200)]
//		[JsonProperty("street")]
//		[JsonPropertyName("street")]
//		[Column(TypeName = "nvarchar(200)")]
//		public string Street { get; set; }

//		[Required]
//		[MaxLength(100)]
//		[JsonProperty("city")]
//		[JsonPropertyName("city")]
//		[Column(TypeName = "nvarchar(100)")]
//		public string City { get; set; }

//		[Required]
//		[MaxLength(20)]
//		[JsonProperty("postcode")]
//		[JsonPropertyName("postcode")]
//		[Column(TypeName = "nvarchar(20)")]
//		public string Postcode { get; set; }

//		[Required]
//		[MaxLength(100)]
//		[JsonProperty("country")]
//		[JsonPropertyName("country")]
//		[Column(TypeName = "nvarchar(100)")]
//		public string Country { get; set; }
//	}

//	[Table("PostMetadatas")]
//	public class PostMetadata
//	{
//		public PostMetadata(int views)
//		{
//			Views = views;
//		}

//		[Key]
//		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//		[JsonProperty("metadataId")]
//		[JsonPropertyName("metadataId")]
//		public int Id { get; set; }

//		[Required]
//		[JsonProperty("views")]
//		[JsonPropertyName("views")]
//		[Column(TypeName = "int")]
//		public int Views { get; set; }

//		public List<SearchTerm> TopSearches { get; } = new();

//		public List<Visits> TopGeographies { get; } = new();

//		public List<PostUpdate> Updates { get; } = new();
//	}

//	[Table("SearchTerms")]
//	public class SearchTerm
//	{
//		public SearchTerm(string term, int count)
//		{
//			Term = term;
//			Count = count;
//		}

//		[Key]
//		[MaxLength(100)]
//		[JsonProperty("term")]
//		[JsonPropertyName("term")]
//		[Column(TypeName = "nvarchar(100)")]
//		public string Term { get; set; }

//		[Required]
//		[JsonProperty("count")]
//		[JsonPropertyName("count")]
//		[Column(TypeName = "int")]
//		public int Count { get; set; }
//	}

//	[Table("Visits")]
//	public class Visits
//	{
//		public Visits(double latitude, double longitude, int count)
//		{
//			Latitude = latitude;
//			Longitude = longitude;
//			Count = count;
//		}

//		[Key]
//		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//		[JsonProperty("visitId")]
//		[JsonPropertyName("visitId")]
//		public int Id { get; set; }

//		[Required]
//		[JsonProperty("latitude")]
//		[JsonPropertyName("latitude")]
//		[Column(TypeName = "float")]
//		public double Latitude { get; set; }

//		[Required]
//		[JsonProperty("longitude")]
//		[JsonPropertyName("longitude")]
//		[Column(TypeName = "float")]
//		public double Longitude { get; set; }

//		[Required]
//		[JsonProperty("count")]
//		[JsonPropertyName("count")]
//		[Column(TypeName = "int")]
//		public int Count { get; set; }

//		[JsonProperty("browsers")]
//		[JsonPropertyName("browsers")]
//		public List<string>? Browsers { get; set; }
//	}

//	[Table("PostUpdates")]
//	public class PostUpdate
//	{
//		public PostUpdate(IPAddress postedFrom, DateTime updatedOn)
//		{
//			PostedFrom = postedFrom;
//			UpdatedOn = updatedOn;
//		}

//		[Key]
//		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//		[JsonProperty("updateId")]
//		[JsonPropertyName("updateId")]
//		public int Id { get; set; }

//		[Required]
//		[JsonProperty("postedFrom")]
//		[JsonPropertyName("postedFrom")]
//		[Column(TypeName = "varchar(50)")]
//		public IPAddress PostedFrom { get; set; }

//		[MaxLength(100)]
//		[JsonProperty("updatedBy")]
//		[JsonPropertyName("updatedBy")]
//		[Column(TypeName = "nvarchar(100)")]
//		public string? UpdatedBy { get; init; }

//		[Required]
//		[JsonProperty("updatedOn")]
//		[JsonPropertyName("updatedOn")]
//		[Column(TypeName = "datetime2")]
//		public DateTime UpdatedOn { get; set; }

//		public List<Commit> Commits { get; } = new();
//	}

//	[Table("Commits")]
//	public class Commit
//	{
//		public Commit(DateTime committedOn, string comment)
//		{
//			CommittedOn = committedOn;
//			Comment = comment;
//		}

//		[Key]
//		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//		[JsonProperty("commitId")]
//		[JsonPropertyName("commitId")]
//		public int Id { get; set; }

//		[Required]
//		[JsonProperty("committedOn")]
//		[JsonPropertyName("committedOn")]
//		[Column(TypeName = "datetime2")]
//		public DateTime CommittedOn { get; set; }

//		[Required]
//		[MaxLength(500)]
//		[JsonProperty("comment")]
//		[JsonPropertyName("comment")]
//		[Column(TypeName = "nvarchar(500)")]
//		public string Comment { get; set; }
//	}
//}
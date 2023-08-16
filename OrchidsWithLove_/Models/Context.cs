using System.Data.Entity;
using System;

namespace OrchidsWithLove_.Models
{
	public class OrchidsContext : DbContext
	{
		public OrchidsContext() : base("Connection")
		{ }

		public static OrchidsContext Create() => new OrchidsContext();
		public DbSet<Flower> Flowers { get; set; }
		public DbSet<FlowersPhoto> FlowersPhotos { get; set; }
	}

	public class Flower
	{
		public Guid Id { get; set; }
		public byte[] Image { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}

	public class FlowersPhoto
	{
		public Guid Id { get; set; }
		public Guid FlowerId { get; set; }
		public byte[] Image { get; set; }
	}
}
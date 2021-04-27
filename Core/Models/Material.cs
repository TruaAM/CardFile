using System;

namespace Core.Models
{
	public class Material
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Content { get; set; }

		public DateTime DateCreate { get; set; }
	}
}

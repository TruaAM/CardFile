using System;

namespace Core.Models
{
	public class Material : BaseEntity
	{
		public string Name { get; set; }

		public string Content { get; set; }

		public DateTime DateCreate { get; set; }

		public byte[] Image { get; set; }
	}
}

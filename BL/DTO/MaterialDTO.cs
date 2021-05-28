using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTO
{
	public class MaterialDTO
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Content { get; set; }

		public DateTime DateCreate { get; set; }

		public byte[] Image { get; set; }
	}
}

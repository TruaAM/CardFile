using System;

namespace Core.Models
{
    public class Material
    {
        public Guid Id { set; get; }

        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime DateCreate { get; set; }
    }
}

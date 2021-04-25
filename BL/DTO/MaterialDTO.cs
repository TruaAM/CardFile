using System;

namespace BL.DTO
{
    public class MaterialDTO
    {
        public Guid Id { set; get; }

        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime DateCreate { get; set; }
    }
}

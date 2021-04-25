using BL.DTO;
using System.Collections.Generic;
using System;

namespace BL.Services.Interfaces
{
    public interface IMaterialService
    {
		IEnumerable<MaterialDTO> GetMaterials();
		MaterialDTO GetMaterial(Guid id);
		void Create(MaterialDTO materialDTO);
		void Update(MaterialDTO materialDTO);
		MaterialDTO Find(Guid id);
		MaterialDTO Delete(Guid id);
	}
}

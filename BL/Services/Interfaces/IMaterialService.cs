using System;
using System.Collections.Generic;
using BL.DTO;

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

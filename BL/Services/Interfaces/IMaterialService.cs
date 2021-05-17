using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTO;

namespace BL.Services.Interfaces
{
	public interface IMaterialService
	{
		IEnumerable<MaterialDTO> GetMaterials();

		Task<MaterialDTO> FindByIdAsync(Guid id);

		Task AddAsync(MaterialDTO materialDTO);

		Task UpdateAsync(MaterialDTO materialDTO);

		Task DeleteByIdAsync(Guid id);

		Task<MaterialDTO> GetByIdAsync(Guid id);
	}
}

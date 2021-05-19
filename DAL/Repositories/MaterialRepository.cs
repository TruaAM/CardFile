using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public class MaterialRepository : IMaterialRepository
	{
		private DBContext db;

		public MaterialRepository(DBContext context)
		{
			this.db = context;
		}

		public IEnumerable<Material> GetAll()
		{
			return db.Materials.ToList();
		}

		public Task<Material> GetAsync(Guid id)
		{
			return Task.FromResult(db.Materials.Find(id));
		}

		public Task CreateAsync(Material product)
		{
			return Task.FromResult(db.Materials.Add(product));
		}

		public void Update(Material product)
		{
			db.Entry(product).State = EntityState.Modified;
		}

		public Task<Material> FindAsync(Guid id)
		{
			var resultData = db.Materials.Where(p => p.Id == id).FirstOrDefault();
			return Task.FromResult(resultData);
		}

		public void Delete(Material material)
		{
			db.Materials.Remove(material);
		}

		public Task DeleteByIdAsync(Guid id)
		{
			Material material = db.Materials.Find(id);
			if (material != null)
			{
				return Task.FromResult(db.Materials.Remove(material));
			}
			return null;
		}

	}
}

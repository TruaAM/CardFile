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
	/// <summary>
	/// This repository has basic functionallity to manipulate with materials-table in database
	/// It is needed to incapsulate logic of working with source of data
	/// </summary>
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

		public Task CreateAsync(Material material)
		{
			return Task.FromResult(db.Materials.Add(material));
		}

		public void Update(Material material)
		{
			db.Entry(material).State = EntityState.Modified;
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

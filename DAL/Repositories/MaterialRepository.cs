using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
	public class MaterialRepository : IRepository<Material>
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

		public Material Get(Guid id)
		{
			return db.Materials.Find(id);
		}

		public void Create(Material product)
		{
			db.Materials.Add(product);
		}

		public void Update(Material product)
		{
			db.Entry(product).State = EntityState.Modified;
		}

		//public IEnumerable<Product> Find(Func<Product, Boolean> predicate)
		//{
			//return db.Products.Where(predicate).ToList();
		//}

		public Material Find(Guid id)
		{
			var resultData = db.Materials.Where(p => p.Id == id).FirstOrDefault();
			return resultData;
		}

		public void Delete(Guid id)
		{
			Material product = db.Materials.Find(id);
			if (product != null)
			{
				db.Materials.Remove(product);
			}
		}
	}
}

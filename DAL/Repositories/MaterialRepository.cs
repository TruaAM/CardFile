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

        public void Create(Material material)
        {
            db.Materials.Add(material);
        }

        public void Delete(Guid id)
        {
            Material material = db.Materials.Find(id);
            if (material != null)
            {
                db.Materials.Remove(material);
            }
        }

        public Material Get(Guid id)
        {
            return db.Materials.Find(id);
        }

        public IEnumerable<Material> GetAll()
        {
            return db.Materials.ToList();
        }

        public void Update(Material material)
        {
            db.Entry(material).State = EntityState.Modified;
        }

        public Material Find(Guid id)
        {
            var resultData = db.Materials.Where(p => p.Id == id).FirstOrDefault();
            return resultData;
        }
    }
}

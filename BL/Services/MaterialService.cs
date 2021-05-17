using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;

namespace BL.Services
{
    public class MaterialService : IMaterialService
    {
        public IUnitOfWork _unitOfWork;

        public MaterialService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void Create(MaterialDTO materialDTO)
        //public async void Create(MaterialDTO materialDTO)
        {
            Material material = new Material
            {
                Name        = materialDTO.Name,
                Content    = materialDTO.Content,
                DateCreate = materialDTO.DateCreate,
            };

            /*
            await Task.Run(() => {
                _unitOfWork.Materials.Create(material);
                _unitOfWork.Save();
            });
            */
            _unitOfWork.Materials.CreateAsync(material);
            _unitOfWork.SaveAsync();
        }

        public IEnumerable<MaterialDTO> GetMaterials()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Material, MaterialDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Material>, List<MaterialDTO>>(_unitOfWork.Materials.GetAll());
        }

        public MaterialDTO GetMaterial(Guid id)
        {
            //TODO: Exception
            //if (id == null)
            //    throw new ValidationException("Не установлено id товара", "");
            //if (product == null)
            //    throw new ValidationException("Товар не найден", "");
            var material = _unitOfWork.Materials.GetAsync(id).Result;
            return new MaterialDTO { Id = material.Id, Name = material.Name, Content = material.Content, DateCreate = material.DateCreate, };
        }

        public void Update(MaterialDTO materialDTO)
        {

            Material dbEntry = _unitOfWork.Materials.FindAsync(materialDTO.Id).Result;
            if (dbEntry != null)
            {
                dbEntry.Name        = materialDTO.Name;
                dbEntry.Content    = materialDTO.Content;
                dbEntry.DateCreate       = materialDTO.DateCreate;
            }
            _unitOfWork.Materials.Update(dbEntry);
            _unitOfWork.SaveAsync();
        }

        public MaterialDTO Find(Guid id)
        {
            var material = _unitOfWork.Materials.FindAsync(id).Result;
            return new MaterialDTO { Id = material.Id, Name = material.Name, Content = material.Content, DateCreate = material.DateCreate, };

        }

        public MaterialDTO Delete(Guid id)
        {
            var material = _unitOfWork.Materials.FindAsync(id).Result;
            if (material != null)
            {
                _unitOfWork.Materials.DeleteByIdAsync(material.Id);
                _unitOfWork.SaveAsync();
            }
            return new MaterialDTO { Id = material.Id, Name = material.Name, Content = material.Content, DateCreate = material.DateCreate, };

        }
    }
}

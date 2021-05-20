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
        public readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _automapper;

        //public MaterialService(IUnitOfWork unitOfWork, Mapper automapper)
        public MaterialService()
        {
            _unitOfWork = new UnitOfWork();
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _automapper = new Mapper(configuration);
        }

        public Task AddAsync(MaterialDTO materialDTO)
        {
            Material material = _automapper.Map<MaterialDTO, Material>(materialDTO);
            _unitOfWork.Materials.CreateAsync(material);
            return Task.FromResult(_unitOfWork.SaveAsync());
        }

        public IEnumerable<MaterialDTO> GetMaterials()
        {
            IEnumerable<Material> allMaterials = _unitOfWork.Materials.GetAll();
            return _automapper.Map<IEnumerable<Material>, IEnumerable<MaterialDTO>>(allMaterials);
        }

        public Task<MaterialDTO> GetByIdAsync(Guid id)
        {
            Material material = _unitOfWork.Materials.GetAsync(id).Result;
            return Task.FromResult(_automapper.Map<Material, MaterialDTO>(material));
        }

        public Task UpdateAsync(MaterialDTO materialDTO)
        {
            Material dbEntry = _unitOfWork.Materials.FindAsync(materialDTO.Id).Result;
            if (dbEntry != null)
            {
                dbEntry.Name = materialDTO.Name;
                dbEntry.Content = materialDTO.Content;
                dbEntry.DateCreate = materialDTO.DateCreate;
            }
            //Material materail = _automapper.Map<MaterialDTO, Material>(materialDTO);
            //_unitOfWork.Materials.Update(materail);
            return Task.FromResult(_unitOfWork.SaveAsync());
        }

        public Task<MaterialDTO> FindByIdAsync(Guid id)
        {
            Material material = _unitOfWork.Materials.FindAsync(id).Result;
            return Task.FromResult(_automapper.Map<Material, MaterialDTO>(material));
        }

        public Task DeleteByIdAsync(Guid id)
        {
            _unitOfWork.Materials.DeleteByIdAsync(id);
            return Task.FromResult(_unitOfWork.SaveAsync());
        }
    }
}

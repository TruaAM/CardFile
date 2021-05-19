﻿using System;
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

        public Task AddAsync(MaterialDTO materialDTO)
        {
            Material material = new Material
            {
                Name        = materialDTO.Name,
                Content    = materialDTO.Content,
                DateCreate = materialDTO.DateCreate,
            };

            _unitOfWork.Materials.CreateAsync(material);
            return Task.FromResult(_unitOfWork.SaveAsync());
        }

        public IEnumerable<MaterialDTO> GetMaterials()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Material, MaterialDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Material>, List<MaterialDTO>>(_unitOfWork.Materials.GetAll());
        }

        public Task<MaterialDTO> GetByIdAsync(Guid id)
        {
            var material = _unitOfWork.Materials.GetAsync(id).Result;
            return Task.FromResult(new MaterialDTO { Id = material.Id, Name = material.Name, Content = material.Content, DateCreate = material.DateCreate, });
        }

        public Task UpdateAsync(MaterialDTO materialDTO)
        {

            Material dbEntry = _unitOfWork.Materials.FindAsync(materialDTO.Id).Result;
            if (dbEntry != null)
            {
                dbEntry.Name        = materialDTO.Name;
                dbEntry.Content    = materialDTO.Content;
                dbEntry.DateCreate       = materialDTO.DateCreate;
            }
            _unitOfWork.Materials.Update(dbEntry);
            return Task.FromResult(_unitOfWork.SaveAsync());
        }

        public Task<MaterialDTO> FindByIdAsync(Guid id)
        {
            var material = _unitOfWork.Materials.FindAsync(id).Result;
            return Task.FromResult(new MaterialDTO { Id = material.Id, Name = material.Name, Content = material.Content, DateCreate = material.DateCreate, });

        }

        public Task DeleteByIdAsync(Guid id)
        {
            _unitOfWork.Materials.DeleteByIdAsync(id);
            return Task.FromResult(_unitOfWork.SaveAsync());
        }
    }
}

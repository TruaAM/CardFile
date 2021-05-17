using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BL.Services.Interfaces;
using BL.DTO;
using AutoMapper;
using PL.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace PL.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        IMaterialService _materialService;

        public AdminController(IMaterialService serv)
        {
            _materialService = serv;
        }
        public IActionResult Index()
        {
            IEnumerable<MaterialDTO> materialDtos = _materialService.GetMaterials();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MaterialDTO, MaterialViewModel>()).CreateMapper();
            var materials = mapper.Map<IEnumerable<MaterialDTO>, List<MaterialViewModel>>(materialDtos);

            if (materials != null)
            {
                return View(materials);
            }
            else
            {
                return NotFound();
            }
        }

        public ViewResult Edit(Guid Id)
        {
            MaterialDTO materialDto = _materialService.FindByIdAsync(Id).Result;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MaterialDTO, MaterialViewModel>()).CreateMapper();
            var material = mapper.Map<MaterialDTO, MaterialViewModel>(materialDto);
            return View(material);
        }

        [HttpPost]
        public ActionResult Edit(MaterialViewModel materialViewModel)
        {

            MaterialDTO materialDto = _materialService.FindByIdAsync(materialViewModel.Id).Result;
            {
                materialDto.Id         = materialViewModel.Id;
                materialDto.Name       = materialViewModel.Name;
                materialDto.Content    = materialViewModel.Content;
                materialDto.DateCreate = materialViewModel.DateCreate;
                //materialDto.DateCreate = DateTime.Now;
            }

            if (ModelState.IsValid)
            {
                _materialService.UpdateAsync(materialDto);
                TempData["message"] = string.Format("Changes in the  \"{0}\" have been saved", materialDto.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(materialViewModel);
            }
        }

        public ViewResult Create()
        {
            MaterialDTO materialDto = new MaterialDTO();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MaterialDTO, MaterialViewModel>()).CreateMapper();
            var material = mapper.Map<MaterialDTO, MaterialViewModel>(materialDto);
            return View(material);
        }

        [HttpPost]
        public ActionResult Create(MaterialViewModel materialViewModel)
        {
            try
            {
                MaterialDTO materialDto = new MaterialDTO
                {
                    Name = materialViewModel.Name,
                    Content = materialViewModel.Content,
                    DateCreate  = materialViewModel.DateCreate,
                    //DateCreate = DateTime.Now,
                };

				materialDto.Id = Guid.NewGuid();

				if (ModelState.IsValid)
				{
                    _materialService.AddAsync(materialDto);
                TempData["message"] = string.Format("The  \"{0}\" has been added", materialDto.Name);
                return RedirectToAction("Index");
			    }
				else
			    {
				    return View(materialViewModel);
			    }
		    }

            catch (/*Validation*/Exception ex)
            {
                //ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(materialViewModel);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
             string nameOfMaterial = _materialService.FindByIdAsync(id).Result.Name;
             
            var deletedMaterial = _materialService.DeleteByIdAsync(id);

             if (deletedMaterial != null)
             {
                TempData["message"] = string.Format("The \"{0}\" has been deleted", nameOfMaterial);
             }
             return RedirectToAction("Index");
        }     
    }
}


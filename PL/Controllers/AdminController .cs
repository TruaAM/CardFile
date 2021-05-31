using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BL.Services.Interfaces;
using BL.DTO;
using AutoMapper;
using PL.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace PL.Controllers
{
    /// <summary>
    /// This controller is for admins only.
    /// Contains all functionality to add, delete, edit materials
    /// </summary>
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IMaterialService _materialService;

        public AdminController(IMaterialService serv)
        {
            _materialService = serv;
        }

        /// <summary>
        /// This method fill table with materials, if they exist, from business layer
        /// </summary>
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

        /// <summary>
        /// This method returns material's data in edit view
        /// </summary>
        public ViewResult Edit(Guid Id)
        {
            MaterialDTO materialDto = _materialService.FindByIdAsync(Id).Result;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MaterialDTO, MaterialViewModel>()).CreateMapper();
            var material = mapper.Map<MaterialDTO, MaterialViewModel>(materialDto);
            return View(material);
        }

        /// <summary>
        /// This method sends new data for chosen material in business layer
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Edit(MaterialViewModel materialViewModel)
        {

            MaterialDTO materialDto = _materialService.FindByIdAsync(materialViewModel.Id).Result;
            {
                materialDto.Id         = materialViewModel.Id;
                materialDto.Name       = materialViewModel.Name;
                materialDto.Content    = materialViewModel.Content;
                materialDto.DateCreate = materialViewModel.DateCreate;
                //materialDto.DateCreate = DateTime.Now;
            }

            if (materialViewModel.ImageIn != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(materialViewModel.ImageIn.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)materialViewModel.ImageIn.Length);
                }
                materialViewModel.Image = imageData;
                materialDto.Image = materialViewModel.Image;
            }

            if (ModelState.IsValid)
            {
                await _materialService.UpdateAsync(materialDto);
                TempData["message"] = string.Format("Changes in the  \"{0}\" have been saved", materialDto.Name);
                return View(materialViewModel);
                //return RedirectToAction("Index");
            }
            else
            {
                return View(materialViewModel);
            }
        }

        /// <summary>
        /// This method create new material to be filled with data
        /// </summary>
        public ViewResult Create()
        {
            MaterialDTO materialDto = new MaterialDTO();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MaterialDTO, MaterialViewModel>()).CreateMapper();
            var material = mapper.Map<MaterialDTO, MaterialViewModel>(materialDto);
            return View(material);
        }

        /// <summary>
        /// This method sends new data for new material in business layer
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create(MaterialViewModel materialViewModel)
        {
            MaterialDTO materialDto = new MaterialDTO
            {
                Name = materialViewModel.Name,
                Content = materialViewModel.Content,
                DateCreate = materialViewModel.DateCreate,
                //DateCreate = DateTime.Now,
            };

            byte[] imageData = null;

            if (materialViewModel.ImageIn != null)
            {
                using (var binaryReader = new BinaryReader(materialViewModel.ImageIn.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)materialViewModel.ImageIn.Length);
                }
            }
            materialViewModel.Image = imageData;
            materialDto.Image = materialViewModel.Image;

            materialDto.Id = Guid.NewGuid();

            if (ModelState.IsValid)
            {
                await _materialService.AddAsync(materialDto);
                TempData["message"] = string.Format("The  \"{0}\" has been added", materialDto.Name);
                return View(materialViewModel);
                //return RedirectToAction("Index");
            }
            else
            {
                return View(materialViewModel);
            }
        }

        /// <summary>
        /// This method sends id of chosen material to business layer that is suppose to be deleted from database
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
             var material = await _materialService.FindByIdAsync(id);
             
             await _materialService.DeleteByIdAsync(id);

             TempData["message"] = string.Format("The \"{0}\" has been deleted", material.Name);
             
             return RedirectToAction("Index");
        }     
    }
}


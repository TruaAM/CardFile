using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BL.Services.Interfaces;
using BL.DTO;
using AutoMapper;
using PL.Models;
using Microsoft.AspNetCore.Authorization;

namespace PL.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class MaterialsController : Controller
    {
        IMaterialService _materialService;

        public MaterialsController(IMaterialService serv)
        {
            _materialService = serv;
        }

        public IActionResult Index()
        {
            IEnumerable<MaterialDTO> materialDtos = _materialService.GetMaterials();
            var mapper   = new MapperConfiguration(cfg => cfg.CreateMap<MaterialDTO, MaterialViewModel>()).CreateMapper();
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

	}
}

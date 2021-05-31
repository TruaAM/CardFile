using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BL.Services.Interfaces;
using BL.DTO;
using AutoMapper;
using PL.Models;
using Microsoft.AspNetCore.Authorization;

namespace PL.Controllers
{
    /// <summary>
    /// This controller is for users and admins.
    /// It allows you to see all materials stored in database
    /// </summary>
    [Authorize(Roles = "admin, user")]
    public class MaterialsController : Controller
    {
        private readonly IMaterialService _materialService;

        public MaterialsController(IMaterialService serv)
        {
            _materialService = serv;
        }

        /// <summary>
        /// This method returns view of MaterialModel with all materials in it (from business layer)
        /// </summary>
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

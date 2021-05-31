using AutoMapper;
using BL.DTO;
using Core.Models;

namespace BL
{
    /// <summary>
    /// Custom automapper for business layer to transfer data from DTO models to CORE models
    /// </summary>
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserDTO>()
                .ReverseMap();
 
            CreateMap<Material, MaterialDTO>()
                .ReverseMap();
        }
    }

}

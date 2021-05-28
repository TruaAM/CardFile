using AutoMapper;
using BL.DTO;
using Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BL
{
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

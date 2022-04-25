using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoyageAPI.Models;
using VoyageAPI.Models.DTOs;
using VoyageAPI.Models.DTOs.Request;
using VoyageAPI.Models.DTOs.Response;

namespace VoyageAPI.MapperProfiles
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<VoyageCreateRequest, Voyage>().ReverseMap();

            CreateMap<VoyageResponse, Voyage>().ReverseMap();

            CreateMap<UserDTO, User>().ReverseMap();

        }
    }
}

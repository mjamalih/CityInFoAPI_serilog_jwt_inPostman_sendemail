﻿using AutoMapper;

namespace CityInFoAPI.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City,Models.CityWithoutPointOfInterestDto>();
            CreateMap<Entities.City,Models.CityDto>();
            CreateMap<Entities.City, Models.CityDto>();
        }
    }
}
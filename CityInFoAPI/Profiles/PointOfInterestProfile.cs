﻿using AutoMapper;

namespace CityInFoAPI.Profiles
{
    public class PointOfInterestProfile:Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDto>();

            CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
            CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>();

        }
    }
}

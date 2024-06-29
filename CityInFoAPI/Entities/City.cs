﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInFoAPI.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        //public City(string name)
        //{
        //    this.Name = name;
        //}

        public ICollection<PointOfInterest> PointOfInterest { get; set; }
        = new List<PointOfInterest>();
    }
}
﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CityInFoAPI.Entities
{
    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        //Relation
        [ForeignKey("CityId")]
        public City? City { get; set; }
        
        public int CityId { get; set; }

        //public PointOfInterest(string name)
        //{
        //    this.Name = name;
        //}
        public string Description { get; set; }
    }
}

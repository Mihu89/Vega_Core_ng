﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }

        public bool IsRegistered { get; set; }
        [Required]
        [StringLength(150)]
        public string ContactName { get; set; }

        [Required]
        [StringLength(20)]
        public string ContactPhone { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Models
{
    public class Products
    {
        // By convention, a property named Id or <type name>Id will be configured as the primary key of an entity.
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Color { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        [Required]
        public int AvailableQuantity { get; set; }
        public int? CategoryId { get; set; } // ? is nullable type
        public Category Category { get; set; }

    }
}

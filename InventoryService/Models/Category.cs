using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Models
{
    public class Category
    {
        // By convention, a property named Id or <type name>Id will be configured as the primary key of an entity.
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Status { get; set; } = true;
        public List<Products> products { get; set; }
    }
}

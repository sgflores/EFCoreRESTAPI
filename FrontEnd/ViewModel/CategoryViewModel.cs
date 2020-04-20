using InventoryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.ViewModel
{
    public class CategoryViewModel : Category
    {
        public string StrStatus { get; set; } = "1"; // Active
    }
}

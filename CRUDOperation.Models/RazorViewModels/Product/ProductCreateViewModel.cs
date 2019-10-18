using CRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDOperation.Models.RazorViewModels.Product
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Please Enter the Product Name!")]
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime? ExpireDate { get; set; }

        public bool IsActive { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Category> CategoyList { get; set; }
        public string CategoryName { get; set; }
        public byte[] ImageUrl { get; set; }
        
        public string ImagePath { get; set; }

        [NotMapped]
        public List<CRUDOperation.Models.Product> ProductList { get; set; }
    }
}

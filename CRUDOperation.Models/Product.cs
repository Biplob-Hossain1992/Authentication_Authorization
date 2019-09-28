using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRUDOperation.Models
{
    public class Product
    {
        
        public long Id { get; set; }

        [Required(ErrorMessage = "Please Enter the Product Name!")]
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime? ExpireDate { get; set; }

        public bool IsActive { get; set; }

        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public byte[] ImageUrl { get; set; }

        public string ImagePath { get; set; }
        public virtual Stock Stock { get; set; }
        //public List<ProductOrder> Orders { get; set; }


    }
}

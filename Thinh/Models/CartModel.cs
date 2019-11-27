using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thinh.Models
{
    public class CartModel
    {
        [StringLength(38)]
        [Display(Name = "Product Id")]
        public string productId { get; set; }

        [StringLength(50)]
        [Display(Name = "Product Code")]
        [Required]
        public string productCode { get; set; }

        [StringLength(254)]
        [Display(Name = "Product Name")]
        [Required]
        public string productName { get; set; }

        [Display(Name = "Product Type Name")]
        public string productTypeName { get; set; }
        [Display(Name = "Product URL")]
        public string productUrl { get; set; }
    }
}

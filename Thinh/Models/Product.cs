using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thinh.Models
{
    public class Product
    {
        //[Key]
        [Display(Name = "Product Id")]
        public int productId { get; set; }

        [StringLength(254)]
        [Display(Name = "Product Name")]
        [Required]
        public string productName { get; set; }

        [Display(Name = "Product Category")]
        public string productCategory { get; set; }
        [Display(Name = "Product URL")]
        public string productUrl { get; set; }

        [Display(Name = "Product Description")]
        public string productDescription { get; set; }

        [Display(Name = "Product Image")]
        public string productImgUrl { get; set; }

        [Display(Name = "Price")]
        public string Price { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? LastEdit { get; set; }

		public int IsApproved { get; set; }
	}
}

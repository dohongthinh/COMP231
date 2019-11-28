using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thinh.Models
{
    public class ApplicationUser : IdentityUser
    {
        //public ApplicationUser(string userName) : base(userName)
        //{
        //}

        //[Required(ErrorMessage = "Please Enter Your Name")]
        //[Display(Name = "FullName")]
        //public string FullName { get; set; }

        //public string Address { get; set; }

        //[Required]
        //[DataType(DataType.Date)]
        //public DateTime? DateOfBirth { get; set; }

        public int IsAdmin { get; set; }
    }
}

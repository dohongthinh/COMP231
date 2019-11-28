using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thinh.Models
{
    public class Feedbacks
    {
        [Key]
        public int FeedbackId { get; set; }
        [Display(Name = "Email")]
        public String Email { get; set; }

        [Display(Name = "Feedback")]
        public String Feedback { get; set; }
    }
}

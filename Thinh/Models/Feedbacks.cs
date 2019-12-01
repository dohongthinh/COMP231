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
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
        public string Email { get; set; }

		[Required]
		[Display(Name = "Feedback")]
        public string Feedback { get; set; }
	}
}

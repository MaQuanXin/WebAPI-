using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        public string LoginName { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 6)]
        public string LoginPwd { get; set; }
    }
}
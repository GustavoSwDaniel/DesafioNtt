using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DesafioNtt.Application.Requests.DTOs
{
    public class UserRegistration
    {
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        [StringLength(255, ErrorMessage = "The password must be at least 8 characters long" , MinimumLength  = 8)]
        public string Password { get; set; }
        
        [Compare(nameof(Password), ErrorMessage = "This field is required")]
        public string ConfirmPassword { get; set; }
    }
}
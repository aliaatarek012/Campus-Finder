﻿using System.ComponentModel.DataAnnotations;

namespace _CampusFinder.Dto
{
    public class RegisterDto
    {

        [Required, MaxLength(50)]
        public string Displayname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(100)]
        //[RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
        //ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 number, 1 non alphanumeric and at least 6 characters")]
        public string Password { get; set; }


    }
}

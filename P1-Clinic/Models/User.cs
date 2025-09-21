using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace P1_Clinic.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }    
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public enum enLang { English = 1, Arabic = 2, French = 3, Greek = 4, Italian = 5, Japanese = 6, Korean = 7 }
        [NotMapped]
        public enLang Lang { get; set; }
       
    }
}
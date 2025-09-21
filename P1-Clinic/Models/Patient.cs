using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace P1_Clinic.Models
{
    public class Patient
    {
         
         
        public int PatientId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
       

    }
}
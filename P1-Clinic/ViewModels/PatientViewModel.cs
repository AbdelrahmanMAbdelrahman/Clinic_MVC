using P1_Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P1_Clinic.ViewModels
{
    public class PatientViewModel
    {
        public int PatientId { get; set; }
        public bool IsPaid { get; set; }
        public Person Person { get; set; }
    }
}
using P1_Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P1_Clinic.ViewModels
{
    public class MedicalRecordViewModel
    {
        public int MedicalRecordId { get; set; }
        public string VisitDescription { get; set; }
        public string Diagnosis { get; set; }
        public string Notes { get; set; }
        public bool IsPaid { get; set; }
        public Patient Patient { get; set; }

 
        public Doctor Doctor { get; set; }
    }
}
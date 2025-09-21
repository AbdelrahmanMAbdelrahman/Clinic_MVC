using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace P1_Clinic.Models
{
    public class MedicalRecord
    {
 
        public int MedicalRecordId { get; set; }
        public string VisitDescription { get; set; }
        public string Diagnosis { get; set; }   
        public string Notes { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
         
    }
}
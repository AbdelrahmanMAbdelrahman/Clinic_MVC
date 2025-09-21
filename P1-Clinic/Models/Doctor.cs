using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace P1_Clinic.Models
{
    public class Doctor
    {
        public Doctor(int doctorId, string specialization, Person person, enMode mode)
        {
            DoctorId = doctorId;
            PersonId = person.PersonId;
            Specialization = specialization;
            this.Person = person;
            Mode = mode;
        }
        public Doctor()
        {
            
        }

        public int DoctorId { get; set; }
        public int PersonId { get; set; }
        public string Specialization { get; set; }
        public Person Person { get; set; }
        public enum enMode { AddNew = 1, Update = 2 }
        [NotMapped]
        public enMode Mode { get; set; }
    }
}
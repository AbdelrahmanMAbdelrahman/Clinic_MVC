using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace P1_Clinic.Models
{
    public class Person
    {
        public Person(int id, string name, string email, string phone, string address, DateTime dateOfBirth, enMode mode=enMode.AddNew)
        {
            PersonId = id;
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            DateOfBirth = dateOfBirth;
            Mode = mode;
        }

        public Person()
        {
            
        }
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }=DateTime.Now;
        [NotMapped]
        public enMode Mode { get; set; }
  
      
        public enum enMode { AddNew=1,Update=2 }
    }
}
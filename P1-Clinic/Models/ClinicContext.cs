using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace P1_Clinic.Models
{
    public class ClinicContext:DbContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }


        public ClinicContext():base("ConnectionString")
       
        {
            
        }
        
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Prescription>  prescriptions { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Language> Languages { get; set; }  

    }
}
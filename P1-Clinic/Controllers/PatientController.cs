using P1_Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using P1_Clinic.ViewModels;
namespace P1_Clinic.Controllers
{
    public class PatientController : Controller
    {
        ClinicContext clinicContext = new ClinicContext();  
        [HttpGet]
        public ActionResult GetAllPatients()
        {
            var patients = clinicContext.Patients.Include(p=>p.Person).ToList();
            return View(patients);
        }
        [HttpGet]
        public ActionResult GetPatient(int Id)
        {
            var patient = clinicContext.Patients.Include(p => p.Person).FirstOrDefault(p => p.PatientId == Id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            var isPaid= clinicContext.Payments.Any(p => p.PatientId == Id);
            var model = new PatientViewModel
            {
              PatientId= patient.PatientId,
              IsPaid= isPaid
                ,Person= patient.Person
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult AddPatient()
        {
            return View("PatientView",new Patient { Person = new Person() });
        }
        [HttpPost]
        public ActionResult AddPatient(Patient patient)
        {
            if (!ModelState.IsValid) { return HttpNotFound(); }
            //clinicContext.Patients.SqlQuery("exec AddNewPatient @p0,@p1,@p2,@p3",
            //    patient.Person.Name, patient.Person.Email, patient.Person.Phone, patient.Person.Address);
            clinicContext.Patients.Add(patient);
            clinicContext.SaveChanges();
    
            return RedirectToAction("GetAllPatients");
        }
        [HttpPost]
        public ActionResult Save(Patient patient)
        {
            if (patient.PatientId == 0) { return AddPatient(patient); }
            else { return UpdatePatient(patient); }
        }
        [HttpGet]
        public ActionResult UpdatePatient(int Id)
        {
            var patient = clinicContext.Patients.Include(p => p.Person).FirstOrDefault(p => p.PatientId == Id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View("PatientView",patient);
        }
        [HttpPost]
        public ActionResult UpdatePatient(Patient patient)
        {
            if (!ModelState.IsValid) { return HttpNotFound(); }
            Patient dbPatient = clinicContext.Patients.Include(p => p.Person).FirstOrDefault(p => p.PatientId == patient.PatientId);
            if (dbPatient == null)
            {
                return HttpNotFound();
            }
            dbPatient.Person.Name = patient.Person.Name;
            dbPatient.Person.Email = patient.Person.Email;
            dbPatient.Person.Phone = patient.Person.Phone;
            dbPatient.Person.Address = patient.Person.Address;
            clinicContext.SaveChanges();
            return RedirectToAction("GetAllPatients");
        }
        [HttpGet]
        public ActionResult DeletePatient(int Id)
        {
            var patient = clinicContext.Patients.Include(p => p.Person).FirstOrDefault(p => p.PatientId == Id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            clinicContext.Patients.Remove(patient);
            clinicContext.SaveChanges();
            return RedirectToAction("GetAllPatients");
        }
        
    }
}
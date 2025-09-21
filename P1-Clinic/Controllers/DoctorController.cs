using P1_Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace P1_Clinic.Controllers
{
    public class DoctorController : Controller
    {
        ClinicContext clinicContext = new ClinicContext();
        // GET: Doctor
        [HttpGet]
    public ActionResult GetAllDoctors()
        {
            var doctors=clinicContext.Doctors.Include(d=>d.Person).ToList();
            return View(doctors);
        }

        [HttpGet]
        public ActionResult GetDoctor(int Id)
        {
            var doctor = clinicContext.Doctors.Include(d => d.Person).FirstOrDefault(d => d.DoctorId == Id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }
        [HttpGet]
        public ActionResult AddDoctor()
        {
            return View("DoctorView", new Doctor { Person = new Person() });
        }
        [HttpPost]
        public ActionResult AddDoctor(Doctor doctor)
        {
            if (!ModelState.IsValid) { return HttpNotFound(); }
            clinicContext.Doctors.Add(doctor);
            clinicContext.SaveChanges();
            return RedirectToAction("GetAllDoctors");
        }
        [HttpGet]
        public ActionResult UpdateDoctor(int Id)
        {
            var doctor = clinicContext.Doctors.Include(d => d.Person).FirstOrDefault(d => d.DoctorId == Id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View("DoctorView", doctor);
        }
        [HttpPost]
        public ActionResult UpdateDoctor(Doctor doctor)
        {
            if (!ModelState.IsValid) { return HttpNotFound(); }
            var doctorInDb = clinicContext.Doctors.Include(d => d.Person).FirstOrDefault(d => d.DoctorId == doctor.DoctorId);
            if (doctorInDb == null) { return HttpNotFound(); }
            doctorInDb.Person.Name = doctor.Person.Name;
            doctorInDb.Person.Email = doctor.Person.Email;
            doctorInDb.Person.Phone = doctor.Person.Phone;
            doctorInDb.Person.Address = doctor.Person.Address;
            doctorInDb.Specialization = doctor.Specialization;
            clinicContext.SaveChanges();
            return RedirectToAction("GetAllDoctors");
        }
        [HttpGet]
        public ActionResult DeleteDoctor(int Id) { 
        var doctor = clinicContext.Doctors.FirstOrDefault(d => d.DoctorId == Id);
            if (doctor == null) { return HttpNotFound(); }
            clinicContext.Doctors.Remove(doctor);
            clinicContext.SaveChanges();
            return RedirectToAction("GetAllDoctors");
        }
        [HttpPost]
        public ActionResult Save(Doctor doctor)
        {
            if (doctor.DoctorId == 0) { return AddDoctor(doctor); }
            else { return UpdateDoctor(doctor); }
        }
    }
}
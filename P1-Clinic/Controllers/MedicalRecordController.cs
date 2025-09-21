using Microsoft.Ajax.Utilities;
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
    public class MedicalRecordController:Controller
    {
        ClinicContext clinicDB = new ClinicContext();
        [HttpGet]
        public ActionResult GetAllMedicalRecords()
        {
            var medicalRecords = clinicDB.MedicalRecords.Include(m=>m.Patient).Include(p=>p.Patient.Person).
                Include(m=>m.Doctor).Include(p=>p.Doctor.Person).ToList();
            return View(medicalRecords);
        }

        [HttpGet]
        public ActionResult GetMedicalRecord(int Id)
        {
            var medicalRecord = clinicDB.MedicalRecords.Include(m => m.Patient).Include(p => p.Patient.Person).
                Include(m => m.Doctor).Include(p => p.Doctor.Person).FirstOrDefault(m=>m.MedicalRecordId==Id);
            if (medicalRecord==null)
            {
                return HttpNotFound();
            }
           
      
            return View(medicalRecord);
        }
        [HttpGet]
        public ActionResult AddNewMedicalRecord()
        {
            ViewBag.Patients = new SelectList((clinicDB.Patients.Include(p => p.Person).ToList()), "PatientId", "Person.Name");
            ViewBag.Doctors=new SelectList(clinicDB.Doctors.Include(d=>d.Person).ToList(),"DoctorId","Person.Name");
            return View("MedicalRecordView", new MedicalRecord{});
        }
        [HttpPost]
        public ActionResult AddNewMedicalRecord(MedicalRecord record)
        {
           bool IsPaid=clinicDB.Payments.Any(p=>p.PatientId==record.PatientId)&&clinicDB.Payments.Any(p=>p.DoctorId==record.DoctorId);
            if (!IsPaid)
            {
                ViewBag.Patients = new SelectList((clinicDB.Patients.Include(p => p.Person).ToList()), "PatientId", "Person.Name");
                ViewBag.Doctors = new SelectList(clinicDB.Doctors.Include(d => d.Person).ToList(), "DoctorId", "Person.Name");
                ModelState.AddModelError("","this patient is not paid for the fees");
                return View("MedicalRecordView", record);
            }
            clinicDB.MedicalRecords.Add(record);
            clinicDB.SaveChanges();
            return RedirectToAction("GetAllMedicalRecords");
        }
        [HttpGet]
        public ActionResult UpdateMedicalRecord(int Id)
        {
            var medicalRecord = clinicDB.MedicalRecords.FirstOrDefault(m => m.MedicalRecordId == Id);
            if (medicalRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.Patients = new SelectList((clinicDB.Patients.Include(p => p.Person).ToList()), "PatientId", "Person.Name");
            ViewBag.Doctors = new SelectList(clinicDB.Doctors.Include(d => d.Person).ToList(), "DoctorId", "Person.Name");
            return View("MedicalRecordView", medicalRecord);
        }

        [HttpPost]
        public ActionResult UpdateMedicalRecord(MedicalRecord record)
        {
            var medicalRecord = clinicDB.MedicalRecords.FirstOrDefault(m => m.MedicalRecordId == record.MedicalRecordId);
            if (medicalRecord == null)
            {
                return HttpNotFound();
            }
            medicalRecord.VisitDescription = record.VisitDescription;
            medicalRecord.Diagnosis = record.Diagnosis;
            medicalRecord.Notes = record.Notes;
            medicalRecord.PatientId = record.PatientId;
            medicalRecord.DoctorId = record.DoctorId;
            
            clinicDB.SaveChanges();
            return RedirectToAction("GetAllMedicalRecords");
        }
        [HttpGet]
        public ActionResult DeleteMedicalRecord(int Id)
        {
            var medicalRecord = clinicDB.MedicalRecords.FirstOrDefault(m => m.MedicalRecordId == Id);
            if (medicalRecord == null)
            {
                return HttpNotFound();
            }
            clinicDB.MedicalRecords.Remove(medicalRecord);
            clinicDB.SaveChanges();
            return RedirectToAction("GetAllMedicalRecords");
        }
        [HttpPost]
        public ActionResult Save(MedicalRecord record)
        {
            if (record.MedicalRecordId == 0)
            {
                return AddNewMedicalRecord(record);
            }
            return UpdateMedicalRecord(record);
        }
    }
}
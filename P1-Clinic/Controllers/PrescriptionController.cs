using P1_Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Http.Results;
namespace P1_Clinic.Controllers
{
    public class PrescriptionController:Controller
    {
        ClinicContext clinicContext = new ClinicContext();
        [HttpGet]
        public ActionResult GetAllPrescriptions()
        {
            var Prescriptions = clinicContext.prescriptions.Include(p=>p.MedicalRecord).Include(
                p=>p.MedicalRecord.Patient).Include(p=>p.MedicalRecord.Patient.Person).
                Include(p=>p.MedicalRecord.Doctor).Include(p => p.MedicalRecord.Doctor.Person)
                .ToList();
            return View(Prescriptions);
        }
        [HttpGet]
        public ActionResult GetPrescription(int id)
        {
            var Prescription = clinicContext.prescriptions.Include(p => p.MedicalRecord).Include(
               p => p.MedicalRecord.Patient).Include(p => p.MedicalRecord.Patient.Person).
               Include(p => p.MedicalRecord.Doctor).Include(p => p.MedicalRecord.Doctor.Person).
               FirstOrDefault(p=>p.PrescriptionId==id);
            return View(Prescription);
        }
        [HttpGet]
        public ActionResult AddNewPrescription()
        {
            var MRS = clinicContext.MedicalRecords.
                Include(m => m.Patient).Include(m => m.Patient.Person).ToList();
            ViewBag.MRs = new SelectList(MRS, "MedicalRecordId", "Patient.Person.Name");
            return View("PrescriptionView", new Prescription());
        }
        [HttpPost]
        public ActionResult AddNewPrescription(Prescription prescription)
        {
            clinicContext.prescriptions.Add(prescription);
            clinicContext.SaveChanges();
            return RedirectToAction("GetAllPrescriptions");
        }
        [HttpGet]
        public ActionResult UpdatePrescription(int Id) {
            var prescription = clinicContext.prescriptions.Find(Id);
            var MRS = clinicContext.MedicalRecords.
                 Include(m => m.Patient).Include(m => m.Patient.Person).ToList();
            ViewBag.MRs = new SelectList(MRS, "MedicalRecordId", "Patient.Person.Name");
            if (prescription == null) {
                return HttpNotFound();
            }
            return View("PrescriptionView", prescription);
        }
        [HttpPost]
        public ActionResult UpdatePrescription(Prescription prescription)
        {
            Prescription DBPrescription = clinicContext.prescriptions.Find(prescription.PrescriptionId);
            if (DBPrescription == null) { return HttpNotFound(); }
            DBPrescription.StartDate = prescription.StartDate;
            DBPrescription.EndDate = prescription.EndDate;
            DBPrescription.Dosage = prescription.Dosage;
            DBPrescription.Notes = prescription.Notes;
            DBPrescription.MedicalRecordId = prescription.MedicalRecordId;
            DBPrescription.Frequency = prescription.Frequency;
            DBPrescription.MedicationName = prescription.MedicationName;
            clinicContext.SaveChanges();
            return RedirectToAction("GetAllPrescriptions");
        }
        [HttpPost]
        public ActionResult Save(Prescription pres)
        {
            if (pres.PrescriptionId == 0)
            {
                return AddNewPrescription(pres);
            }
            return UpdatePrescription(pres);
        }
        [HttpGet]
        public ActionResult DeletePrescription(int Id) {
            Prescription pres = clinicContext.prescriptions.Find(Id);
            if (pres == null) { HttpNotFound(); }
            clinicContext.prescriptions.Remove(pres);
            clinicContext.SaveChanges();
            return RedirectToAction("GetAllPrescriptions");
        }
    }
}
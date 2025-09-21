using P1_Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace P1_Clinic.Controllers
{
    public class PaymentController: Controller
    {
        ClinicContext clinicDB = new ClinicContext();
        [HttpGet]
        public ActionResult GetAllPayments()
        {
            var payments = clinicDB.Payments.Include(p=>p.PaymentMethod).Include(p=>p.Patient).
                Include(p=>p.Patient.Person).Include(p=>p.Doctor).Include(p=>p.Doctor.Person).ToList();
            return View(payments);
        }
        [HttpGet]
        public ActionResult GetPaymentBy(int Id)
        {
            var payment = clinicDB.Payments.Include(p => p.PaymentMethod).
                Include(p => p.Patient).
                Include(p => p.Patient.Person).
                Include(p=>p.Doctor).
                Include(p=>p.Doctor.Person)
                .FirstOrDefault(p => p.PaymentId == Id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        [HttpGet]
        public ActionResult AddNewPayment()
        {
            ViewBag.PaymentMethods = new SelectList(clinicDB.PaymentMethods, "PaymentMethodId", "MethodName");
            ViewBag.Patients=new SelectList(clinicDB.Patients.Include(p=>p.Person).ToList(),"PatientId","Person.Name");
            ViewBag.Doctors=new SelectList(clinicDB.Doctors.Include(d=>d.Person).ToList(),"DoctorId","Person.Name");
            return View();
        }
        [HttpPost]  
        public ActionResult AddNewPayment(Payment payment)
        {
           
                clinicDB.Payments.Add(payment);
                clinicDB.SaveChanges();
                return RedirectToAction("GetAllPayments");
          
  
        }
 

    }
}
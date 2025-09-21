using P1_Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P1_Clinic.Controllers
{
    public class PaymentMethodController : Controller
    {
        ClinicContext clinicDB=new ClinicContext();
        [HttpGet]
public ActionResult GetAllPaymentMethods()
        {
            var paymentMethods = clinicDB.PaymentMethods.ToList();  
            return View(paymentMethods);
        }
    }
}
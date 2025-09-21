using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P1_Clinic.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string MethodName { get; set; }
        public string Description { get; set; } = "null";
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamAccessValidator.Models
{
    public class PaymentResponse
    {
        public bool Etat { get; set; }
        public string Message { get; set; }
        public string Matricule { get; set; }
        public object Montant { get; set; }
    }
}

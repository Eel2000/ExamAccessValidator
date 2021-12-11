using System;
using System.Text;
using System.Collections.Generic;

namespace ExamAccessValidator.Models
{
    public class AccessReponse
    {
        public bool Etat { get; set; }
        public string Message { get; set; }
        public int Access { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ExamAccessValidator.Models
{
    public class Scanner
    {
        public string Matricule { get; set; }
        public DateTime ScannedDate { get; set; }
        public bool CanBeScannedTwice { get; set; }
    }
}

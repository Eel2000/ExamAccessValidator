using ExamAccessValidator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExamAccessValidator.Interfaces
{
    public interface IValidatorService
    {
        Task<AccessReponse> GetAccessValidation(string matricule);
        Task<PaymentResponse> GetPaymentValidation(string matricule);
    }
}

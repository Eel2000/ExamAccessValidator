using ExamAccessValidator.AppHelper;
using ExamAccessValidator.Interfaces;
using ExamAccessValidator.Models;
using System.Threading.Tasks;

namespace ExamAccessValidator.Service
{
    public class ValidatorService : IValidatorService
    {
        public async Task<AccessReponse> GetAccessValidation(string matricule)
        {
            return await BaseService.GetAsync<AccessReponse>(Constants.URL_SET_STUDENT_CURRENT_EXAM_ATTANDACE, matricule);
        }

        public async Task<PaymentResponse> GetPaymentValidation(string matricule)
        {
            return await BaseService.GetAsync<PaymentResponse>(Constants.URL_CHECK_STUDENT_PAYMENTS, matricule);
        }
    }
}

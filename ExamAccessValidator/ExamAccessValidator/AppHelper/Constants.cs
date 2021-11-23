using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExamAccessValidator.AppHelper
{
    /// <summary>
    /// Every app constant and important setting which does not changes everytime. 
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// the base url to the APIs.
        /// </summary>
        private const string BASE_URL = "http://esistfc.000webhostapp.com/";


        /// <summary>
        /// The endpoint to the base url for checking student fees regulation(regalated)
        /// </summary>
        private const string check_student_endpoint = "api_paiement.php?matricule=";

        /// <summary>
        /// The endpoint to the base url for setting the student current exam attandence.
        /// </summary>
        private const string set_student_exam_attendance_endpoint = "api_absence.php?matricule=";


        #region URL_TO_CALL_IN_APP

        /// <summary>
        /// URL to check student payements completion.
        /// </summary>
        public static string URL_CHECK_STUDENT_PAYMENTS = BASE_URL + check_student_endpoint;

        public static string URL_SET_STUDENT_CURRENT_EXAM_ATTANDACE = BASE_URL + set_student_exam_attendance_endpoint;

        #endregion
    }
}

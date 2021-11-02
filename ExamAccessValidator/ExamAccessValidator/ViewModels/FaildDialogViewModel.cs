using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamAccessValidator.ViewModels
{
    public class FaildDialogViewModel : BaseViewModel
    {
        private string _scanResult;

        public string ScanResult
        {
            get => _scanResult;
            set => SetProperty(ref _scanResult, value);
        }

        public FaildDialogViewModel(string result = null)
        {
            ScanResult = result;
        }
    }
}

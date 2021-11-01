using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Rg.Plugins.Popup.Services;

namespace ExamAccessValidator.ViewModels
{
    public class SuccessDialogViewModel : BaseViewModel
    {
        private string _scanResult;

        public string ScanResult
        {
            get => _scanResult;
            set => SetProperty(ref _scanResult, value);
        }

        public SuccessDialogViewModel(string message = null)
        {
            ScanResult = message;
        }
    }
}

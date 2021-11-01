using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using System.Linq;
using ExamAccessValidator.Views.Dialogs;

namespace ExamAccessValidator.ViewModels
{
    public class ScannerPageViewModel : BaseViewModel
    {
        private bool _isScanning;
        private bool _isVisible;


        public bool IsScanning
        {
            get => _isScanning;
            set => SetProperty(ref _isScanning, value);
        }

        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        public ScannerPageViewModel(bool isScanning = true)
        {
            IsScanning = true;
            DetectQrCodeCommand = new Command(async (args) =>
            {
                try
                {
                    var scanResult = (ZXing.Result)args;
                    System.Diagnostics.Debug.WriteLine(scanResult.Text, "Scanner");

                    //await App.Current.MainPage.DisplayAlert("Scanner", $"the result of scan is {scanResult.Text}", "ok");
                    await ClosePopups();
                    await PopupNavigation.Instance.PushAsync(new SuccessDialog(scanResult.Text));
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e, "Scanner");
                    // _toast.ShowLong("An Error occur while scannig please retry again");
                    IsScanning = false;
                    await ClosePopups();
                }
            });
        }

        public ICommand DetectQrCodeCommand { get; set; }

        public async Task ClosePopups()
        {
            var stack = PopupNavigation.Instance.PopupStack;
            if (stack.Any())
            {
                await PopupNavigation.Instance.PopAllAsync();
            }

        }
    }
}

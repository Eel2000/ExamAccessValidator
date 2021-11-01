using ExamAccessValidator.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using ExamAccessValidator.Views.Dialogs;
using System.Threading.Tasks;

namespace ExamAccessValidator.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IToastMessage _toast;



        public MainPageViewModel(INavigationService navigationService, IToastMessage toast)
            : base(navigationService)
        {
            Title = "Validator";
            _toast = toast;

            //IsScanning = false;
            //IsVisible = false;


            ScanCommand = new Command(async () =>
            {
                var photoAndVideoPermissions = await Permissions.RequestAsync<Permissions.Photos>();
                if (photoAndVideoPermissions == PermissionStatus.Granted)
                {
                    _toast.ShowLong($"Scanne started...");
                    await OpenScannerAsync();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Scanner", "Permision to take pictures and video denied. app will not be able to scan.", "ok");
                }
            });
        }


        public ICommand ScanCommand { get; set; }

        protected async Task OpenScannerAsync()
        {
            var stack = PopupNavigation.Instance.PopupStack;
            if (!stack.Any())
            {
                await PopupNavigation.Instance.PushAsync(new ScannerPage());
            }
            else if (stack.Any())
                _toast.ShowLong("The scanner is already running");
        }
    }
}

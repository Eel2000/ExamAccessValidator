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
        private readonly IValidatorService _validatorService;

        public MainPageViewModel(INavigationService navigationService, IToastMessage toast, IValidatorService validatorService)
            : base(navigationService)
        {
            Title = "Validator";
            _toast = toast;
            _validatorService = validatorService;

            //IsScanning = false;
            //IsVisible = false;

            ScanCommand = new Command(async () =>
            {
                await OpenScannerAsync();
                _toast.ShowLong($"Scanne started...");
            });

            StartCommand = new Command(async () =>
            {
                App.Permission = await Permissions.RequestAsync<Permissions.Camera>();
                App.StoragePermission = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (App.Permission == PermissionStatus.Granted && App.StoragePermission == PermissionStatus.Granted)
                {
                    _toast.ShowLong("Vous pouvez scanner maintenant...");
                }
            });
        }

        //TODO: Check the permission if before launching the scanner :Done

        public ICommand ScanCommand { get; set; }
        public ICommand StartCommand { get; set; }

        protected async Task OpenScannerAsync()
        {
            if (App.Permission == PermissionStatus.Denied || App.Permission == PermissionStatus.Unknown || App.Permission == PermissionStatus.Disabled)
            {
                await App.Current.MainPage.DisplayAlert("VALIDATOR", "Vous n'avez pas l'autorisation du device pour scanner. veuillez d'abord lancer le service", "OK");
                return;
            }

            var stack = PopupNavigation.Instance.PopupStack;
            if (!stack.Any())
            {
                await PopupNavigation.Instance.PushAsync(new ScannerPage(_validatorService, App.Permission));
            }
            else if (stack.Any())
                _toast.ShowLong("The scanner is already running");
        }
    }
}

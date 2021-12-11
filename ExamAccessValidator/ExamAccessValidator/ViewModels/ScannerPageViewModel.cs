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
using ExamAccessValidator.Interfaces;
using ExamAccessValidator.Models;
using Xamarin.Essentials;

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

        int counte = 0;

        public ScannerPageViewModel(IValidatorService service, PermissionStatus permission = PermissionStatus.Denied, bool isScanning = true)
        {
            IsScanning = true;
            DetectQrCodeCommand = new Command(async (args) =>
            {
                if (permission == PermissionStatus.Granted)
                {
                    try
                    {
                        await ClosePopups();

                        var scanResult = (ZXing.Result)args;
                        System.Diagnostics.Debug.WriteLine(scanResult.Text, "Scanner");

                        var isAlreadyScanned = ExamAccessValidator.Service.LiteDB.Database.GetCollection<Scanner>().Query()
                            .Where(x => x.Matricule == scanResult.Text).FirstOrDefault();
                        if (isAlreadyScanned == null)
                        {
                            await PopupNavigation.Instance.PushAsync(new LoadingDialogs());
                            var query = "1";
                            if (scanResult.Text.StartsWith("https"))
#pragma warning disable CS1717 // Assignment made to same variable
                                query = query;
#pragma warning restore CS1717 // Assignment made to same variable
                            else
                                query = scanResult.Text;

                            var payment = await service.GetPaymentValidation(query);
                            var attendance = await service.GetAccessValidation(query);

                            if (payment.Etat && attendance.Etat)
                            {
                                var scanned = new Scanner
                                {
                                    Matricule = payment.Matricule,
                                    ScannedDate = DateTime.Today,
                                    CanBeScannedTwice = true
                                };

                                //var userDialog = await App.Current.MainPage.DisplayAlert("Configurations", "Voulez-vous autoriser se badge a etre scanner 2 fois aujourd'hui?", "OUI", "OUI");
                                // if (userDialog)
                                //     scanned.CanBeScannedTwice = true;
                                // else
                                //     scanned.CanBeScannedTwice = false;

                                Service.LiteDB.Database.GetCollection<Scanner>().Insert(scanned);
                                await ClosePopups();

                                await PopupNavigation.Instance.PushAsync(new SuccessDialog($"Le matricule {payment.Matricule} est {attendance.Message}"));

                            }
                            else
                            {
                                await ClosePopups();
                                await PopupNavigation.Instance.PushAsync(new FailedDialog($"vous n'etes pas authorisé {attendance.Message}"));
                            }
                        }

                        if (isAlreadyScanned != null && isAlreadyScanned.CanBeScannedTwice)
                        {
                            await PopupNavigation.Instance.PushAsync(new LoadingDialogs());
                            var payment = await service.GetPaymentValidation(scanResult.Text);
                            var attendance = await service.GetAccessValidation(scanResult.Text);
                            if (payment.Etat && attendance.Etat)
                            {
                                isAlreadyScanned.CanBeScannedTwice = false;
                                Service.LiteDB.Database.GetCollection<Scanner>().DeleteMany(x => x.Matricule == isAlreadyScanned.Matricule);
                                Service.LiteDB.Database.GetCollection<Scanner>().Insert(isAlreadyScanned);
                                await ClosePopups();
                                await PopupNavigation.Instance.PushAsync(new SuccessDialog($"Le matricule {payment.Matricule} est {attendance.Message}"));
                            }
                        }
                        if (isAlreadyScanned != null && isAlreadyScanned.CanBeScannedTwice == false)
                            await App.Current.MainPage.DisplayAlert("Validator", "Validator", "Ce badge à déjà été scanner.");


                        #region old
                        //switch (scanResult.Text)
                        //{
                        //    case "1":
                        //        await PopupNavigation.Instance.PushAsync(new SuccessDialog("You can passe this exam, good luck."));
                        //        break;
                        //    case "2":
                        //        await PopupNavigation.Instance.PushAsync(new FailedDialog("You're not allow to pass this exam, it seems like you've not completed everything."));
                        //        break;
                        //    default:
                        //        System.Diagnostics.Debug.WriteLine("Out of range", "Scanner");
                        //        break;
                        //}
                        #endregion
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e, "Scanner");
                        IsScanning = false;
                        await ClosePopups();
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("permission denied.");
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

        protected void Alert()
        {
            App.Current.MainPage.DisplayAlert("ALERT", "Vous n'avez pas l'autorisation du device pour scanner. veuillez d'abord lancer le service", "OK");
        }
    }
}

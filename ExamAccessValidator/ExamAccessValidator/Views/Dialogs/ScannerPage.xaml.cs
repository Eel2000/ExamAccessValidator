using ExamAccessValidator.Interfaces;
using ExamAccessValidator.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamAccessValidator.Views.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : PopupPage
    {
        public ScannerPage(IValidatorService service)
        {
            InitializeComponent();

            BindingContext = new ScannerPageViewModel(service);
        }
    }
}
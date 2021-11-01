using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Rg.Plugins.Popup.Pages;
using ExamAccessValidator.ViewModels;

namespace ExamAccessValidator.Views.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuccessDialog : PopupPage
    {
        public SuccessDialog(string message = null)
        {
            InitializeComponent();
            BindingContext = new SuccessDialogViewModel(message);
        }
    }
}
using ExamAccessValidator.Interfaces;
using ExamAccessValidator.Service;
using ExamAccessValidator.ViewModels;
using ExamAccessValidator.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace ExamAccessValidator
{
    public partial class App
    {
        public static PermissionStatus Permission { get; set; }
        public static PermissionStatus StoragePermission { get; set; }
        public static PermissionStatus InternetPermission { get; set; }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            containerRegistry.Register<IValidatorService, ValidatorService>();
        }
    }
}

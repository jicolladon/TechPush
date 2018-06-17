using Prism.Mvvm;
using Prism.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TechPush.ViewModels.Base
{
    public class BaseViewModel : BindableBase, INavigationAware
    {

        protected readonly INavigationService navigationService;

        public BaseViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public void OnNavigatedFrom(NavigationParameters parameters) { }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = (string)parameters["title"] + " and Prism";
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }

        private string _infoIsBusy;

        public string InfoIsBusy
        {
            get { return _infoIsBusy; }
            set { SetProperty(ref _infoIsBusy, value); }
        }

    }
}

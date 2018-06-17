using Prism.Navigation;
using System;
using Prism.Commands;
using Prism.Mvvm;

using System.Windows.Input;
using TechPush.ViewModels.Base;
using Xamarin.Forms;
using TechPush.Core.Services;
using TechPush.Helper;
using TechPush.Core.Models;
using Prism.Services;

namespace TechPush.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {

        public MainPageViewModel(INavigationService navigationService, IUserService userService, IPageDialogService dialogService) : base(navigationService)
        {
            this.userService = userService;
            this.dialogService = dialogService;
            MessagingCenter.Subscribe<RegisterCompleted>(CommonHelper.MessagingCenterGOD, "RegisterCompleted", RegisterCompletedCommand);
            InitData();
        }

        private async void RegisterCompletedCommand(RegisterCompleted obj)
        {
            await navigationService.NavigateAsync("PrincipalPage");
        }



        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                SetProperty(ref _username, value);
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
            }
        }


        private bool _rembemberMe;

        public bool RememberMe
        {
            get { return _rembemberMe; }
            set { SetProperty(ref _rembemberMe, value); }
        }


        private ICommand _loginCommand;
        public ICommand LoginCommnad
        {
            get
            {
                _loginCommand = _loginCommand ?? new Command(LoginAction);
                return _loginCommand;
            }
        }

        private readonly IUserService userService;
        private readonly IPageDialogService dialogService;
        private ICommand _registerCommand;
        private ICommand _pasionaClickedCommand;

        public ICommand RegisterCommand
        {
            get
            {
                _registerCommand = _registerCommand ?? new Command(RegisterAction);
                return _registerCommand;
            }
        }

        public ICommand PasionaClickedCommand
        {
            get
            {
                _pasionaClickedCommand = _pasionaClickedCommand ?? new Command(PasionaTapped);
                return _pasionaClickedCommand;
            }
        }

       

        private async void RegisterAction(object obj)
        {
            if (!IsBusy)
            {
                IsBusy = true;
                await navigationService.NavigateAsync("RegisterPage");
                IsBusy = false;
            }

        }

        private async void LoginAction(object obj)
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                try
                {
                    if (!IsBusy)
                    {
                        IsBusy = true;

                        var result = await userService.LoginUser(new Core.Dto.UserDto()
                        {
                            Username = Username,
                            Password = Password,
                            RememberMe = RememberMe,
                        });
                        if (result)
                        {
                            await navigationService.NavigateAsync("PrincipalPage");
                        }
                        else
                        {
                            await dialogService.DisplayAlertAsync("Error", "Usuario o contraseña invalidos", "Cerrar");

                        }
                    }
                }
                catch (Exception)
                {
                    await dialogService.DisplayAlertAsync("Error", "Se ha producido un error", "Cerrar");

                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        private void InitData()
        {
            var user = userService.GetLastUser();
            if (user != null)
            {
                Username = user.Username;
                Password = user.Password;
                RememberMe = user.RememberMe;
                if(RememberMe && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                {
                    LoginAction(null);
                }
            }

        }

        private void PasionaTapped(object obj)
        {
            Device.OpenUri(new Uri("http://www.pasiona.com/empleo/"));
        }



    }
}

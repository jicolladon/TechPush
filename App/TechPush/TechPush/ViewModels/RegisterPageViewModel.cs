using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using TechPush.Core.Dto;
using TechPush.Core.Models;
using TechPush.Core.Services;
using TechPush.Helper;
using TechPush.ViewModels.Base;
using Xamarin.Forms;

namespace TechPush.ViewModels
{
    public class RegisterPageViewModel : BaseViewModel
    {

        public RegisterPageViewModel(INavigationService navigationService, IUserService userService,
                                        IPageDialogService dialogService) : base(navigationService)
        {
            this.userService = userService;
            this.dialogService = dialogService;
            InitData();
        }

        private ObservableCollection<string> _genders;

        public ObservableCollection<string> Genders
        {
            get { return _genders; }
            set { SetProperty(ref _genders, value); }
        }

        private ObservableCollection<int> _ageList;

        public ObservableCollection<int> AgeList
        {
            get { return _ageList; }
            set { SetProperty(ref _ageList, value); }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                SetProperty(ref _username, value);
                EvalueButton();
            }
        }



        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
                EvalueButton();
            }
        }

        private int _ageSelected;

        public int AgeSelected
        {
            get { return _ageSelected; }
            set
            {
                SetProperty(ref _ageSelected, value);
                EvalueButton();
            }
        }

        private string _genderSelected;
        private IUserService userService;
        private readonly IPageDialogService dialogService;

        public string GenderSelected
        {
            get { return _genderSelected; }
            set
            {
                SetProperty(ref _genderSelected, value);
                EvalueButton();
            }
        }

        private bool _canExecuteButton;

        public bool CanExecuteButton
        {
            get { return _canExecuteButton; }
            set { SetProperty(ref _canExecuteButton, value); }
        }

        private bool _rememberMe;

        public bool RememberMe
        {
            get { return _rememberMe; }
            set { SetProperty(ref _rememberMe, value); }
        }



        private ICommand _registerCommand;

        public ICommand RegisterCommand
        {
            get
            {
                _registerCommand = _registerCommand ?? new Command(RegisterAction);
                return _registerCommand;
            }
        }

        private async void RegisterAction(object obj)
        {
            var errors = IsValidRegister();
            if (errors.Any())
            {
                await dialogService.DisplayAlertAsync("Error", string.Join("\n", errors), "Cerrar");
            }
            else
            {

                try
                {
                    if (!IsBusy)
                    {
                        IsBusy = true;
                        bool result = await userService.RegisterUser(new UserDto()
                        {
                            Username = Username,
                            Password = Password,
                            Age = AgeSelected,
                            Gender = GenderSelected ?? string.Empty,
                            RememberMe = RememberMe,
                        });
                        if (result)
                        {
                            await navigationService.GoBackAsync();
                            MessagingCenter.Send<RegisterCompleted>(new RegisterCompleted
                            {
                                Success = true,
                            }, "RegisterCompleted");

                        }
                        else
                        {
                            await dialogService.DisplayAlertAsync("Error", "Error al registrar usuario", "Cerrar");

                        }
                    }
                }
                catch (Exception e)
                {
                    await dialogService.DisplayAlertAsync("Error", "Error al registrar usuario", "Cerrar");
                }
                finally
                {
                    IsBusy = false;
                }
            }
            
        }

        private List<string> IsValidRegister()
        {
            var errors = new List<string>();
            if(string.IsNullOrEmpty(Username) || !IsValidEmail(Username))
            {
                errors.Add("El correo electronico no es valido.");
            }
            else if(string.IsNullOrEmpty(Password))
            {
                errors.Add("El campo password es obligatorio");

            }
            return errors;
        }



        private void InitData()
        {
            Genders = new ObservableCollection<string>() { "Hombre", "Mujer", "Prefiero no decirlo" };
            AgeList = new ObservableCollection<int>(Enumerable.Range(14, 100).ToList());
        }

        private void EvalueButton()
        {
            CanExecuteButton = !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password?.Trim());
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}

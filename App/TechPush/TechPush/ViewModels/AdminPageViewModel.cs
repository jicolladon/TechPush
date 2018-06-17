using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Plugin.Media;
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
    public class AdminPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        private readonly IPageDialogService _dialogService;

        public AdminPageViewModel(INavigationService navigationService, 
                                    IPushService pushService,
                                    IUserService userService, IPageDialogService iPageDialogService) : base(navigationService)
        {
            _pushService = pushService;
            this._userService = userService;
            this._dialogService = iPageDialogService;
            InitData();
        }
        #region properties

        private ObservableCollection<SelectableElement<string>> _genders;

        public ObservableCollection<SelectableElement<string>> Genders
        {
            get { return _genders; }
            set { SetProperty(ref _genders, value); }
        }

        private ObservableCollection<SelectableElement<int>> _Ages;

        public ObservableCollection<SelectableElement<int>> Ages
        {
            get { return _Ages; }
            set { SetProperty(ref _Ages, value); }
        }

        private ObservableCollection<SelectableElement<string>> _tags;

        public ObservableCollection<SelectableElement<string>> Tags
        {
            get { return _tags; }
            set { SetProperty(ref _tags, value); }
        }

        private bool _all;

        public bool All
        {
            get { return _all; }
            set
            {
                SetProperty(ref _all, value);
                SelectAll();
            }
        }


        private bool _random;

        public bool Random
        {
            get { return _random; }
            set
            {
                SetProperty(ref _random, value);
                SetActiveSelectors();
            }
        }

        private bool _isActiveSelector = true;

        public bool IsActiveSelector
        {
            get { return _isActiveSelector; }
            set { SetProperty(ref _isActiveSelector, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }



        #endregion
        private ICommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                _refreshCommand = _refreshCommand ?? new Command(RefreshCommandTapped);
                return _refreshCommand;
            }
        }

        private ICommand _sendNotificationCommand;
        public ICommand SendNotificationCommand
        {
            get
            {
                _sendNotificationCommand = _sendNotificationCommand ?? new Command(SendCommandTapped);
                return _sendNotificationCommand;
            }
        }

        private ICommand _addImageNotificationCommand;
        public ICommand AddImageNotificationCommand
        {
            get
            {
                _addImageNotificationCommand = _addImageNotificationCommand ?? new Command(AddImage);
                return _addImageNotificationCommand;
            }
        }

        private byte[] _image;
        private IPushService _pushService;


        public byte[] Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        private void SetActiveSelectors()
        {
            IsActiveSelector = !Random;
        }


        private void SelectAll()
        {
            SelectAllOnList(Tags, All);
            SelectAllOnList(Ages, All);
            SelectAllOnList(Genders, All);

        }

        private void SelectAllOnList<T>(ObservableCollection<SelectableElement<T>> list, bool value)
        {
            foreach (var element in list)
            {
                element.Selected = value;
            }
        }

        private void RefreshCommandTapped(object obj)
        {
            InitData();
        }

        private async void InitData()
        {
            try
            {
                if (!IsBusy)
                {
                    AdminInfoDto data = await _userService.GetAdminInfo();
                    if (data != null)
                    {
                        Ages = GetElements(data.Age);
                        Tags = GetElements(data.Tags);
                        Genders = GetElements(data.Genders);
                        Message = "@Techdencias #4Sessions";
                    }
                    else
                    {
                        Ages = new ObservableCollection<SelectableElement<int>>();
                        Tags = new ObservableCollection<SelectableElement<string>>();
                        Genders = new ObservableCollection<SelectableElement<string>>();

                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

        private ObservableCollection<SelectableElement<T>> GetElements<T>(List<T> tags)
        {
            var result = new ObservableCollection<SelectableElement<T>>();
            foreach (var element in tags)
            {
                result.Add(new SelectableElement<T>()
                {
                    Value = element,
                });
            }
            return result;
        }

        private async void AddImage(object obj)
        {
            var list = new List<IActionSheetButton>();
            list.Add(ActionSheetButton.CreateButton("Galeria", AddImageFromGallery));
            list.Add(ActionSheetButton.CreateButton("Camara", AddImageFromCamera));

            await _dialogService.DisplayActionSheetAsync("Añadir image", list.ToArray());
        }

        private async void AddImageFromCamera()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await _dialogService.DisplayAlertAsync("Error", "La camara no esta disponible", "Cerrar");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Techdencias",
                Name = $"{Guid.NewGuid().ToString()}.png",
                CompressionQuality = 90,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
            });

            if (file == null)
                return;

            Image = CommonHelper.ReadFully(file.GetStream());
        }

        private async void AddImageFromGallery()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await _dialogService.DisplayAlertAsync("Error", "La galeria no esta disponible", "Cerrar");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions()
            {
                CompressionQuality = 90,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
            });

            if (file == null)
                return;

            Image = CommonHelper.ReadFully(file.GetStream());
        }

        private async void SendCommandTapped(object obj)
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    bool result = await _pushService.SendPushNotifications(new PushMessageDto
                    {
                        Ages = Ages.Where(x=>x.Selected).Select(x=>x.Value).ToList(),
                        Image = Image,
                        Genders = Genders.Where(x => x.Selected).Select(x => x.Value).ToList(),
                        Date = DateTime.Now,
                        Description = Message,
                        SelectRandom = Random,
                        Tag = Tags.Where(x=>x.Selected).Select(x=>x.Value).ToList(),
                        Title = $"Notificación enviada por {_userService.GetLastUser().Username}"
                    });
                    if (result)
                    {
                        Clean();
                    }
                    else
                    {
                        await _dialogService.DisplayAlertAsync("Error", "No se pudo enviar la notificación", "Cerrar");
                    }

                }
            }
            catch (Exception e)
            {
                await _dialogService.DisplayAlertAsync("Error", e.Message, "Cerrar");

            }
            finally
            {
                IsBusy = false;
            }
        }

        private void Clean()
        {
            Message = string.Empty;
            Image = null;

        }
    }
}

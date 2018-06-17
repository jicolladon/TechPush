using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.SimpleAudioPlayer;
using Prism.Navigation;
using Prism.Services;
using TechPush.Core.Dto;
using TechPush.Core.Mapper;
using TechPush.Core.Models;
using TechPush.Core.Services;
using TechPush.Helper;
using TechPush.ViewModels.Base;
using Xamarin.Forms;

namespace TechPush.ViewModels
{
    public class PrincipalPageViewModel : BaseViewModel
    {


        private ICommand _adminCommand;
        private readonly IUserService userService;
        private readonly IEntityMapper entityMapper;
        private readonly IPageDialogService dialogService;
        private readonly Core.Services.IDeviceService deviceService;
        private readonly IPushService pushService;
        private readonly ISimpleAudioPlayer _player;

        public ICommand AdminCommand
        {
            get
            {
                _adminCommand = _adminCommand ?? new Command(AdminAction);
                return _adminCommand;
            }
        }

        public ICommand CloseSession
        {
            get
            {
                _closeSession = _closeSession ?? new Command(Close);
                return _closeSession;
            }
        }

        public ICommand ItemTappedCommand
        {
            get
            {
                _itemTappedCommand = _itemTappedCommand ?? new Command<object>(ItemTapped);
                return _itemTappedCommand;
            }
        }



        private ObservableCollection<NotificationModel> _notifications;
        private bool _intruderMode;

        public ObservableCollection<NotificationModel> Notifications
        {
            get { return _notifications; }
            set { SetProperty(ref _notifications, value); }
        }

        private bool _showDeniedImage;

        public bool ShowDeniedImage
        {
            get { return _showDeniedImage; }
            set { SetProperty(ref _showDeniedImage, value); }
        }

        private NotificationModel _selectedNotification;

        public NotificationModel SelectedNotification
        {
            get { return _selectedNotification; }
            set { SetProperty(ref _selectedNotification, value); }
        }

        private bool _showNotification;

        public bool ShowNotification
        {
            get { return _showNotification; }
            set { SetProperty(ref _showNotification, value); }
        }



        public PrincipalPageViewModel(INavigationService navigationService,
                                        IUserService userService,
                                        IEntityMapper entityMapper,
                                        IPageDialogService dialogService,
                                        Core.Services.IDeviceService deviceService,
                                        IMessage messageService,
                                        IPushService pushService) : base(navigationService)
        {
            _messageService = messageService;
            this.userService = userService;
            this.entityMapper = entityMapper;
            this.dialogService = dialogService;
            this.deviceService = deviceService;
            this.pushService = pushService;
            _player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;

            InitData();
        }

        private bool _isAdmin;
        private ICommand _closeSession;
        private ICommand _goBackCommand;

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { SetProperty(ref _isAdmin, value); }
        }

        public ICommand GoBackCommand
        {
            get
            {
                _goBackCommand = _goBackCommand ?? new Command(CloseApp);
                return _goBackCommand;
            }
        }

        public ICommand CloseShowNotificationCommand
        {
            get
            {
                _CloseShowNotificationCommand = _CloseShowNotificationCommand ?? new Command(CloseNotification);
                return _CloseShowNotificationCommand;
            }
        }

        public ICommand ShareCommand
        {
            get
            {
                _shareCommand = _shareCommand ?? new Command(Sharetapped);
                return _shareCommand;
            }
        }



        private void CloseNotification(object obj)
        {
            ShowNotification = false;
            SelectedNotification = null;
        }

        public async void InitData()
        {
            Suscribe();
            await RegisterPush();
            var user = userService.GetLastUser();
            IsAdmin = user.IsAdmin;
            await LoadData();
        }

        private void Suscribe()
        {
            MessagingCenter.Subscribe<string>(this, CommonHelper.UpdateListRequests, UpdateListRequests);
        }

        private async void UpdateListRequests(string obj)
        {
            await RefreshData();
        }

        private async Task LoadData()
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    var data = await userService.GetNotifications();
                    if (data != null && data.Any())
                    {
                        var result = entityMapper.Map<IEnumerable<MessageDto>, IEnumerable<NotificationModel>>(data);
                        Notifications = new ObservableCollection<NotificationModel>(result.OrderByDescending(x => x.Date));
                    }
                    else
                    {
                        Notifications = new ObservableCollection<NotificationModel>();

                    }
                }
            }
            catch (Exception e)
            {
                //ignored
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RegisterPush()
        {
            try
            {
                var user = userService.GetLastUser();
                if (user != null)
                {
                    await pushService.RegisterPush(user);
                }
            }
            catch (Exception e)
            {
                //ignore
            }
        }

        private async Task RefreshData()
        {
            try
            {
                if (!_intruderMode)
                {
                    var data = await userService.GetNotifications();
                    if (data != null && data.Any())
                    {
                        var result = entityMapper.Map<IEnumerable<MessageDto>, IEnumerable<NotificationModel>>(data);
                        var toAdd = result.Where(x => !Notifications.Any(y => y.Guid == x.Guid)).OrderBy(x => x.Date);
                        foreach (var element in toAdd)
                        {
                            Notifications.Insert(0, element);
                        }
                        if (toAdd.Count() > 0)
                        {
                            _messageService.ShortAlert("Notificaciones actualizadas");
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        private async void Close(object obj)
        {
            userService.RemoveLoguedUser();
            await navigationService.GoBackAsync();
        }

        private async void CloseApp(object obj)
        {
            if (ShowNotification)
            {
                ShowNotification = false;
            }
            else
            {
                var result = await dialogService.DisplayAlertAsync("Alerta", "¿Estas seguro que deseas cerrar la aplicación", "Aceptar", "Cancelar");
                if (result)
                {
                    deviceService.CloseApp();
                }
            }
        }

        private int _clicCounter = -1;
        private IMessage _messageService;
        private ICommand _CloseShowNotificationCommand;
        private ICommand _itemTappedCommand;
        private ICommand _shareCommand;
        private async void AdminAction(object obj)
        {
            if (!IsAdmin)
            {
                _clicCounter++;
                if (_clicCounter == 5)
                {
                    _clicCounter = 0;
                    ActiveIntruderMode();
                }
                else
                {
                    _messageService.ShortAlert(CommonHelper.IntruderMode[_clicCounter]);
                }
            }
            else
            {
                await navigationService.NavigateAsync("AdminPage");
            }
        }

        private void ActiveIntruderMode()
        {
            _intruderMode = true;
            Notifications = new ObservableCollection<NotificationModel>();
            IntruderMode().ConfigureAwait(false);
        }


        private async Task IntruderMode()
        {
            int timer = CommonHelper.IntruderModeTimer;
            for (int second = 0; second < timer; second++)
            {
                Notifications.Insert(0, new NotificationModel()
                {
                    Date = DateTime.Now,
                    Message = "You didn't say the magic word",
                    Tag = "Error"
                });
                if (second == 5)
                {
                    PlaySound();
                    ShowDeniedImage = true;
                }

                await Task.Delay(500);
            }
            _player.Stop();
            ShowDeniedImage = false;
            _intruderMode = false;
            await LoadData();
        }

        private void PlaySound()
        {
            _player.Load(GetStreamFromFile("magicwrd.wav"));
            _player.Play();
        }

        private Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("TechPush.Resources." + filename);
            return stream;
        }

        private void ItemTapped(object obj)
        {
            var notifitaModel = obj as NotificationModel;
            if (notifitaModel != null)
            {
                SelectedNotification = Notifications.SingleOrDefault(x => x.Guid == notifitaModel.Guid);
                ShowNotification = true;
            }
        }

        private async void Sharetapped(object obj)
        {
            try
            {
                if (!IsBusy && SelectedNotification != null)
                {
                    IsBusy = true;
                    await deviceService.Share(SelectedNotification.Tag, $"{SelectedNotification.Message}", SelectedNotification.Image);
                }
            }
            catch (Exception e)
            {
                await dialogService.DisplayAlertAsync("Error", "Se ha producido un error", "Cancelar");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private ImageSource GetImage(byte[] val)
        {
            ImageSource result;
            if (val != null && val.Length > 0)
            {
                result = ImageSource.FromStream(() => new MemoryStream(val));
            }
            else
            {
                result = ImageSource.FromFile(ResourceHelper.GetImage("noimage.png"));
            }

            return result;

        }
    }
}

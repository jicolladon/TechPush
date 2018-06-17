using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TechPush.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class LoadingControl : Grid
    {
        private string _loadingStringValue;

        public LoadingControl()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty InfoBusyProperty = BindableProperty.Create("InfoBusy",
                                                                                           typeof(string),
                                                                                           typeof(LoadingControl),
                                                                                           string.Empty,
                                                                                           BindingMode.TwoWay,
                                                                                          null,
                                                                                           InfoBusyChanged);

        private static void InfoBusyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as LoadingControl;
            if (control != null)
            {
                control._loadingStringValue = (string)newvalue;
                ShowPercentage(control);
            }
        }

        private static void ShowPercentage(LoadingControl control)
        {
            if (control.ShowProgressBar)
            {
                var stringPercentage = (control.ValueProgressBar * 100).ToString("##");
                if (string.IsNullOrEmpty(stringPercentage))
                {
                    stringPercentage = "0";
                }
                control.lblInfoBusy.Text = $"{control._loadingStringValue} ({stringPercentage}%)";
            }
            else
            {
                control.lblInfoBusy.Text = control._loadingStringValue;
            }
        }

        public string InfoBusy
        {
            get
            {
                return (string)base.GetValue(InfoBusyProperty);
            }
            set
            {
                base.SetValue(InfoBusyProperty, value);
            }
        }

        public static readonly BindableProperty ShowProgressBarProperty = BindableProperty.Create("ShowProgressBar",
            typeof(bool),
            typeof(LoadingControl),
            default(bool),
            BindingMode.TwoWay,
            null,
            ShowProgressBarChanged);

        private static void ShowProgressBarChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as LoadingControl;
            if (control != null)
            {
                control.ProgressBarLoading.IsVisible = (bool)newvalue;
            }
        }

        public bool ShowProgressBar
        {
            get
            {
                return (bool)base.GetValue(ShowProgressBarProperty);
            }
            set
            {
                base.SetValue(ShowProgressBarProperty, value);
            }
        }

        public static readonly BindableProperty ValueProgressBarProperty = BindableProperty.Create("ValueProgressBar",
            typeof(double),
            typeof(LoadingControl),
            default(double),
            BindingMode.TwoWay,
            null,
            ValueProgressBarChanged);

        private static void ValueProgressBarChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as LoadingControl;
            if (control != null)
            {
                control.ProgressBarLoading.Progress = (double)newvalue;
                ShowPercentage(control);
            }
        }
        public double ValueProgressBar
        {
            get
            {
                return (double)base.GetValue(ValueProgressBarProperty);
            }
            set
            {
                base.SetValue(ValueProgressBarProperty, value);
            }
        }

        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create("IsBusy",
                                                                                           typeof(bool),
                                                                                           typeof(LoadingControl),
                                                                                           false,
                                                                                           BindingMode.TwoWay,
                                                                                          null,
                                                                                           IsBusyChanged);

        private static void IsBusyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as LoadingControl;
            if (control != null)
            {
                control.ShowTimerIsBusy((bool)newvalue);

            }
        }

        private readonly TimeSpan _timeToShowDialog = new TimeSpan(0, 0, 0, 0, 30);

        private async void ShowTimerIsBusy(bool isVisible)
        {
            if (isVisible)
            {
                await Task.Delay(_timeToShowDialog).ContinueWith((r) =>
                {
                    if (IsBusy)
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            stackBusy.IsVisible = IsBusy;
                            IsVisible = IsBusy || ShowDialog;
                        });
                    }
                });
            }
            else
            {
                stackBusy.IsVisible = false;
                IsVisible = ShowDialog;
            }
        }

        public bool IsBusy
        {
            get
            {
                return (bool)base.GetValue(IsBusyProperty);
            }
            set
            {
                base.SetValue(IsBusyProperty, value);
            }
        }

        public static readonly BindableProperty IsCancelProperty = BindableProperty.Create("IsCancel",
                                                                                         typeof(bool?),
                                                                                         typeof(LoadingControl),
                                                                                         null,
                                                                                         BindingMode.TwoWay,
                                                                                        null,
                                                                                         IsCancelChanged);

        private static void IsCancelChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as LoadingControl;
            if (control != null)
            {
                //control.DialogControl.CancelVisibility = (bool)newvalue;
            }
        }

        public bool? IsCancel
        {
            get
            {
                return (bool?)base.GetValue(IsCancelProperty);
            }
            set
            {
                base.SetValue(IsCancelProperty, value);
            }
        }
        public static readonly BindableProperty ShowDialogProperty = BindableProperty.Create("ShowDialog",
                                                                                           typeof(bool),
                                                                                           typeof(LoadingControl),
                                                                                           false,
                                                                                           BindingMode.TwoWay,
                                                                                           null,
                                                                                           ShowDialogChanged);

        private static void ShowDialogChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as LoadingControl;
            if (control != null)
            {
                control.StackError.IsVisible = (bool)newvalue;
                control.IsVisible = (bool)newvalue;

            }
        }
        public bool ShowDialog
        {
            get
            {
                return (bool)base.GetValue(ShowDialogProperty);
            }
            set
            {
                base.SetValue(ShowDialogProperty, value);
            }
        }


        public static readonly BindableProperty AcceptCommandProperty = BindableProperty.Create("AcceptCommand",
                                                                                            typeof(ICommand),
                                                                                            typeof(LoadingControl),
                                                                                            null,
                                                                                            BindingMode.TwoWay,
                                                                                            null,
                                                                                            AcceptCommandChanged);

        private static void AcceptCommandChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as LoadingControl;
            if (control != null)
            {
                var command = newvalue as ICommand;
                if (command != null)
                {
                    //control.DialogControl.AcceptCommand = command;
                }
            }
        }

        public ICommand AcceptCommand
        {
            get
            {
                return (ICommand)base.GetValue(AcceptCommandProperty);
            }
            set
            {
                base.SetValue(AcceptCommandProperty, value);
            }
        }

        public static readonly BindableProperty CancelCommandProperty = BindableProperty.Create("CancelCommand",
                                                                                            typeof(ICommand),
                                                                                            typeof(LoadingControl),
                                                                                            null,
                                                                                            BindingMode.TwoWay,
                                                                                            null,
                                                                                            CancelCommandChanged);

        private static void CancelCommandChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as LoadingControl;
            if (control != null)
            {
                var command = newvalue as ICommand;
                if (command != null)
                {
                    //control.DialogControl.CancelCommand = command;
                }
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return (ICommand)base.GetValue(CancelCommandProperty);
            }
            set
            {
                base.SetValue(CancelCommandProperty, value);
            }
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title",
                                                                                            typeof(string),
                                                                                            typeof(LoadingControl),
                                                                                            default(string),
                                                                                            BindingMode.TwoWay,
                                                                                            null, TitlePropertyChanged);

        private static void TitlePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as LoadingControl;
            if (control != null)
            {
                var stringValue = newvalue as string;
                if (!string.IsNullOrEmpty(stringValue))
                {
                    //control.DialogControl.Title = stringValue;
                }
            }
        }

        public string Title
        {
            get
            {
                return (string)base.GetValue(TitleProperty);
            }
            set
            {
                base.SetValue(TitleProperty, value);
            }
        }
        public static readonly BindableProperty MessageProperty = BindableProperty.Create("Message",
                                                                                           typeof(string),
                                                                                           typeof(LoadingControl),
                                                                                           default(string),
                                                                                           BindingMode.TwoWay,
                                                                                           null, MessagePropertyChanged);

        private static void MessagePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as LoadingControl;
            if (control != null)
            {
                var stringValue = newvalue as string;
                if (!string.IsNullOrEmpty(stringValue))
                {
                    //control.DialogControl.Message = stringValue;
                }
            }
        }

        public string Message
        {
            get
            {
                return (string)base.GetValue(MessageProperty);
            }
            set
            {
                base.SetValue(MessageProperty, value);
            }
        }
    }
}
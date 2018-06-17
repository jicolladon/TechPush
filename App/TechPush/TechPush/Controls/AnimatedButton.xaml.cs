using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TechPush.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TechPush.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class AnimatedButton : StackLayout
    {
        public AnimatedButton()
        {
            InitializeComponent();
            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async (o) => { await ButtonPressed(); })
            });
        }

        private bool _isPressed = false;

        private async Task ButtonPressed()
        {
            await Animate();
            if (ClickCommand != null && ClickCommand.CanExecute(ClickCommandParameter))
            {
                ClickCommand.Execute(ClickCommandParameter);
            }
        }

        private async Task Animate()
        {
            if (!_isPressed)
            {
                _isPressed = true;
                Opacity = CommonHelper.OpacityAnimation;
                await this.ScaleTo(CommonHelper.ScaleAnimation, CommonHelper.AnimationTimeMilisec, Easing.CubicOut);
                await this.ScaleTo(CommonHelper.ScaleFinalAnimation, CommonHelper.AnimationTimeMilisec,
                    Easing.CubicIn);
                Opacity = CommonHelper.OpacityFinalAnimation;
                _isPressed = false;
            }

        }

        public static readonly BindableProperty ClickCommandProperty = BindableProperty.Create("ClickCommand", typeof(ICommand), typeof(AnimatedButton), null, BindingMode.TwoWay, null, ClickCommandChanged);

        private static void ClickCommandChanged(BindableObject bindable, object oldvalue, object newvalue)
        {

        }

        public ICommand ClickCommand
        {
            get
            {
                return (ICommand)base.GetValue(ClickCommandProperty);
            }
            set
            {
                base.SetValue(ClickCommandProperty, value);
            }
        }

        public static readonly BindableProperty ClickCommandParameterProperty = BindableProperty.Create("ClickCommandParameter", typeof(object), typeof(AnimatedButton), null, BindingMode.TwoWay, null, ClickCommandParameterChanged);

        private static void ClickCommandParameterChanged(BindableObject bindable, object oldvalue, object newvalue)
        {

        }

        public object ClickCommandParameter
        {
            get
            {
                return (object)base.GetValue(ClickCommandParameterProperty);
            }
            set
            {
                base.SetValue(ClickCommandParameterProperty, value);
            }
        }

        public static readonly BindableProperty ButtonTemplateProperty = BindableProperty.Create("ButtonTemplate", typeof(View), typeof(AnimatedButton), default(View), BindingMode.TwoWay, null, ButtonTemplateChanged);
        private static void ButtonTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (StackLayout)bindable;
            var currentView = (View)newValue;
            control.Children.Clear();
            control.Children.Add(currentView);
        }
        public View ButtonTemplate
        {
            get
            {
                return (View)base.GetValue(ButtonTemplateProperty);
            }
            set
            {
                base.SetValue(ButtonTemplateProperty, value);
            }
        }

    }
}
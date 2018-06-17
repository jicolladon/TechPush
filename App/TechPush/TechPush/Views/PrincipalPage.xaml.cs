using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TechPush.Views.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TechPush.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrincipalPage : BaseViewExtended
	{
		public PrincipalPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasBackButton(this, false);
            this.SetBinding(PrincipalPage.GoBackCommandProperty, "GoBackCommand");
		}

        public static readonly BindableProperty GoBackCommandProperty = BindableProperty.Create("GoBackCommand",
                                                                                            typeof(ICommand),
                                                                                            typeof(PrincipalPage),
                                                                                            null,
                                                                                            BindingMode.TwoWay,
                                                                                            null,
                                                                                            null);
        public ICommand GoBackCommand
        {
            get
            {
                return (ICommand)base.GetValue(GoBackCommandProperty);
            }
            set
            {
                base.SetValue(GoBackCommandProperty, value);
            }
        }

        protected override bool OnBackButtonPressed()
        {

            GoBackCommand.Execute(null);
            return true;
        }

        protected override void SetPropertionalThickness(double height, double width)
        {
            base.SetPropertionalThickness(height, width);
            double marginsLateral = (width * 0.3) / 2;
            BaseMargin = new Thickness(marginsLateral, 0, marginsLateral, 0);
        }

	    private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
	    {
            //DO Nothing
	    }

	    private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        throw new NotImplementedException();
	    }
	}
}
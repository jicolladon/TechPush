using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechPush.Views.Base;
using Xamarin.Forms;

namespace TechPush.Views
{
	public partial class MainPage : BaseViewExtended
	{
		public MainPage()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private Thickness _baseMargin;

        public Thickness BaseMargin
        {
            get { return _baseMargin; }
            set
            {
                _baseMargin = value;
                OnPropertyChanged();
            }
        }
        protected override void SetPropertionalThickness(double height, double width)
        {
            base.SetPropertionalThickness(height, width);
            double marginsLateral = (width * 0.3) / 2;
            BaseMargin = new Thickness(marginsLateral, 0, marginsLateral, 0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechPush.Views.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TechPush.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminPage : BaseViewExtended
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private double _ImageWidth;

        public double ImageWidth
        {
            get { return _ImageWidth; }
            set
            {
                _ImageWidth = value;
                OnPropertyChanged();
            }
        }

        protected override void OnSizeChanged(object sender, EventArgs eventArgs)
        {
            base.OnSizeChanged(sender, eventArgs);
            ImageWidth = (Width * 2) / 3;
        }

    }
}
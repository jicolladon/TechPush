using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechPush.Core.Enum;
using Xamarin.Forms;

namespace TechPush.Views.Base
{
	public class BaseViewExtended : ContentPage, IDisposable
	{
        public BaseViewExtended()
        {
            SizeChanged += OnSizeChanged;
        }

        #region Properties

        public static readonly BindableProperty PageOrientationProperty =
                      BindableProperty.Create("PageOrientation", typeof(PageOrientationEnum), typeof(BaseViewExtended),
                          default(PageOrientationEnum), BindingMode.TwoWay, null, null);


        public PageOrientationEnum PageOrientation
        {
            get { return (PageOrientationEnum)GetValue(PageOrientationProperty); }
            set
            {
                SetValue(PageOrientationProperty, value);
            }
        }

        private double _pageHeight;

        public double PageHeight
        {
            get { return _pageHeight; }
            set
            {
                _pageHeight = value;
                OnPropertyChanged();
            }
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



        #endregion

        #region Events
        protected virtual void OnSizeChanged(object sender, EventArgs eventArgs)
        {

            if (!(Width > 0) || !(Height > 0)) return;
            PageHeight = Height;
            SetPropertionalThickness(Height, Width);
            if (Width > Height && PageOrientation != PageOrientationEnum.Landscape)
            {
                //"landscape"
                PageOrientation = PageOrientationEnum.Landscape;
            }
            else if (Width <= Height && PageOrientation != PageOrientationEnum.Portrait)
            {
                PageOrientation = PageOrientationEnum.Portrait;
            }
        }

        protected virtual void SetPropertionalThickness(double height, double width)
        {

        }
        #endregion

        #region dispose
        protected bool Disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                try
                {
                    this.BindingContext = null;
                    this.Content = null;
                    this.SizeChanged -= OnSizeChanged;
                }
                catch (Exception)
                {
                    //ignore
                }
            }
            Disposed = true;
        }
        #endregion
    }
}

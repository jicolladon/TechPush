using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TechPush.Controls
{
    public class CustomListStack : StackLayout
    {

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(ICollection), typeof(CustomListStack)
            , default(ICollection), BindingMode.TwoWay, null, OnItemsSourceChanged);
        private static void OnItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var bin = (CustomListStack)bindable;
            bin?.DrawControl();
        }

        private void DrawControl()
        {
            Children.Clear();
            if (ItemTemplate == null || ItemsSource == null)
                return;
            foreach (var element in ItemsSource)
            {
                var view = ItemTemplate.CreateContent() as View;
                view.BindingContext = element;
                Children.Add(view);
            }


        }

        public DataTemplate ItemTemplate { get; set; }


        public ICollection ItemsSource
        {
            get { return (ICollection)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

    }

}

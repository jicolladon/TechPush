using System;
using System.Collections.Generic;
using System.Text;

namespace TechPush.Core.Models
{
    public class SelectableElement<T> : BaseModel
    {
        private bool _selected;

        public bool Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }

        private T _value;

        public T Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }


    }
}

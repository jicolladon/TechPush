using System;
using System.Collections.Generic;
using System.Text;

namespace TechPush.Core.Models
{
    public class NotificationModel : BaseModel
    {
        public Guid Guid { get; set; }
        private byte[] _image;

        public byte[] Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        private string _tag;

        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }



    }
}

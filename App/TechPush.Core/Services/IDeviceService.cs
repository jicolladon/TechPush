using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechPush.Core.Enum;
using Xamarin.Forms;

namespace TechPush.Core.Services
{
    public interface IDeviceService
    {
        string GetToken();
        DeviceTypeEnum GetDeviceType();
        void CloseApp();

        Task Share(string subject, string message, byte[] image);
    }
}

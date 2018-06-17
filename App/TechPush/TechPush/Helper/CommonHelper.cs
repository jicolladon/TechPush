using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TechPush.Helper
{
    public static class CommonHelper
    {
        public static object MessagingCenterGOD = new object();
        public static double OpacityAnimation = 0.8;
        public static double OpacityFinalAnimation = 1;
        public static double ScaleAnimation = 0.95;
        public static double ScaleFinalAnimation = 1;
        public static uint AnimationTimeMilisec = 30;

        public static string UpdateListRequests = "UpdateListRequests";

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public const int IntruderModeTimer = 15;
        public static List<string> IntruderMode = new List<string>()
        {
            "Solo administradores.",
            "No insistas, no eres administrad@r.",
            "No es no!",
            "Estás siendo muy insistente, habrá consecuencias.",
            "Es la última vez que te aviso."
        };
    }
}

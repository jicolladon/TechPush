using System;
using System.Collections.Generic;
using System.Text;

namespace TechPushCoreApi.Services.Intefaces
{
    public interface ICipher
    {
        string Encrypt(string plainText, string password);
        string Decrypt(string encryptedText, string password);
    }
}

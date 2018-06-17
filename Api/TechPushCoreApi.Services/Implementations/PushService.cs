using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using TechPushCoreApi.Data.Repositories.Interfaces;
using TechPushCoreApi.Services.Entities;
using TechPushCoreApi.Services.Enum;
using TechPushCoreApi.Services.Intefaces;
using PushSharp.Apple;
using PushSharp.Core;
using PushSharp.Google;
using PushSharp.Windows;
using TechPushCoreApi.Data.DbModels;

namespace TechPushCoreApi.Services.Implementations
{
    public class PushService : IPushService
    {
        private IConfiguration _configuration;
        private IOptions<ServiceSettings> _settings;

        public PushService(IOptions<ServiceSettings> settings)
        {
            _settings = settings;
        }


        public void SendNotification(PushSendEntity entity, string rootPath)
        {
            if (entity != null)
            {
                foreach (var user in entity.Users)
                {
                    var androidDevices = user.PushRegistration.Where(x => x.PlatformType == "Android");
                    var iosDevices = user.PushRegistration.Where(x => x.PlatformType == "IOS");
                    if (androidDevices != null && androidDevices.Any())
                    {
                        SendAndroidNotification(entity.Title, entity.Message, androidDevices);
                    }

                    if (iosDevices != null && iosDevices.Any())
                    {
                        SendIosNotifications(entity.Title, entity.Message, iosDevices, rootPath);
                    }
                }
            }
        }


        private bool SendAndroidNotification(string title,string message, IEnumerable<PushRegistration> elements)
        {
            var res = true;
            try
            {
                var config = new GcmConfiguration(null, _settings.Value.AndroidToken, null)
                {
                    GcmUrl = "https://fcm.googleapis.com/fcm/send"

                };

                // Create a new broker
                var gcmBroker = new GcmServiceBroker(config);

                // Wire up events
                gcmBroker.OnNotificationFailed += (notification, aggregateEx) =>
                {

                    aggregateEx.Handle(ex =>
                    {

                        // See what kind of exception it was to further diagnose
                        if (ex is GcmNotificationException)
                        {
                            var notificationException = (GcmNotificationException)ex;

                            // Deal with the failed notification
                            var gcmNotification = notificationException.Notification;
                            var description = notificationException.Description;

                            Console.WriteLine($"GCM Notification Failed: ID={gcmNotification.MessageId}, Desc={description}");
                        }
                        else if (ex is GcmMulticastResultException)
                        {
                            var multicastException = (GcmMulticastResultException)ex;

                            foreach (var succeededNotification in multicastException.Succeeded)
                            {
                                Console.WriteLine($"GCM Notification Succeeded: ID={succeededNotification.MessageId}");
                            }

                            foreach (var failedKvp in multicastException.Failed)
                            {
                                var n = failedKvp.Key;
                                var e = failedKvp.Value;

                                Console.WriteLine($"GCM Notification Failed: ID={n.MessageId}, Desc={message}");
                            }

                        }
                        else if (ex is DeviceSubscriptionExpiredException)
                        {
                            var expiredException = (DeviceSubscriptionExpiredException)ex;

                            var oldId = expiredException.OldSubscriptionId;
                            var newId = expiredException.NewSubscriptionId;

                            Console.WriteLine($"Device RegistrationId Expired: {oldId}");

                            if (!string.IsNullOrWhiteSpace(newId))
                            {
                                // If this value isn't null, our subscription changed and we should update our database
                                Console.WriteLine($"Device RegistrationId Changed To: {newId}");
                            }
                        }
                        else if (ex is RetryAfterException)
                        {
                            var retryException = (RetryAfterException)ex;
                            // If you get rate limited, you should stop sending messages until after the RetryAfterUtc date
                            Console.WriteLine($"GCM Rate Limited, don't send more until after {retryException.RetryAfterUtc}");
                        }
                        else
                        {
                            Console.WriteLine("GCM Notification Failed for some unknown reason");
                        }

                        // Mark it as handled
                        return true;
                    });
                };

                gcmBroker.OnNotificationSucceeded += (notification) =>
                {
                    Console.WriteLine("GCM Notification Sent!");
                };

                // Start the broker
                gcmBroker.Start();
                
                var jsonObject = JObject.Parse(
                    "{" +
                    "\"title\" : \"" + title + "\"," +
                    "\"body\" : \"" + message + "\"," +
                    "\"sound\" : \"mySound.caf\"" +
                    "}");
                foreach (var element in elements)
                {
                    // Queue a notification to send
                    gcmBroker.QueueNotification(new GcmNotification
                    {
                        RegistrationIds = new List<string> { element.Token },
                        Notification = jsonObject
                    });
                }

                // Stop the broker, wait for it to finish   
                // This isn't done after every message, but after you're
                // done with the broker
                gcmBroker.Stop();

            }
            catch (Exception)
            {
                res = false;
            }
            return res;

        }

        private bool SendIosNotifications(string title, string message, IEnumerable<PushRegistration> elements, string rootPath)
        {
            var res = true;
            try
            {
                var pathCert = Path.Combine(rootPath, _settings.Value.IOSCertificatePath);
                var pass = _settings.Value.IOSPassCertificate;
                var type = _settings.Value.IOSTypeCertificate;
                ApnsConfiguration.ApnsServerEnvironment typeofCert = ApnsConfiguration.ApnsServerEnvironment.Production;
                if (type.ToUpper().Equals("SANDBOX"))
                {
                    typeofCert = ApnsConfiguration.ApnsServerEnvironment.Sandbox;
                }
                var config = new ApnsConfiguration(typeofCert, pathCert, pass);
                var apnsBroker = new ApnsServiceBroker(config);
                apnsBroker.Start();

                JObject jsonObject;


                jsonObject = JObject.Parse("{ \"aps\" : { \"alert\" : {\"title\" : \"" + title + "\", \"body\" :\"" + message + "\"} }}");


                foreach (var pushRegistration in elements)
                {

                    apnsBroker.QueueNotification(new ApnsNotification
                    {
                        DeviceToken = pushRegistration.Token,
                        Payload = jsonObject
                    });

                    apnsBroker.OnNotificationFailed += (notification, exception) =>
                    {
                        Debug.WriteLine(exception);
                    };

                    apnsBroker.OnNotificationSucceeded += (notification) =>
                    {
                        Debug.WriteLine(notification);
                    };
                }
                apnsBroker.Stop();
            }

            catch (Exception e)
            {
                res = false;
            }
            return res;
        }
    }
}

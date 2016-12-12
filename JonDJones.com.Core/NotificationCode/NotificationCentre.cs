﻿using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace JonDJones.com.Core.NotificationCode
{
    public class NotificationCentre
    {
        private static readonly Lazy<NotificationCentre> _instance = new Lazy<NotificationCentre>(() => new NotificationCentre(GlobalHost.ConnectionManager.GetHubContext<NotificationHub>().Clients));

        private readonly List<Notification> _notifications = new List<Notification>();

        private NotificationCentre(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            _notifications = new List<Notification>
            {
                new Notification()
            };
        }

        public static NotificationCentre Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients { get; set; }

        public IEnumerable<Notification> GetAllSystemNotifications()
        {
            return _notifications;
        }

        public void AddNewNotification()
        {
            _notifications.Add(new Notification());
            Clients.All.updateNotifications(_notifications);
        }

        public void MarkNotificationAsRead()
        {
            _notifications.ForEach(x => x.Read = true);
            Clients.All.updateNotifications(_notifications);
        }

        public void DeleteAllNotifications()
        {
            _notifications.Clear();
            Clients.All.updateNotifications(_notifications);
        }
    }
}
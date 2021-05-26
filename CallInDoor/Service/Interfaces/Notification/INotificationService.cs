using Domain.DTO.Notification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.Notification
{
    public interface INotificationService
    {
        Task<bool> SendAcceptWidrwalRequestNotifications(SendNotificationVM model);
        Task<bool> SendRejectWidrwalRequestNotifications(SendNotificationVM model);

    }
}

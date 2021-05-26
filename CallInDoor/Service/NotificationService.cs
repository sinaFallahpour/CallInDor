using Domain.DTO.Notification;
using Domain.Entities;
using Service.Interfaces.Notification;
using Service.Interfaces.SmsService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class NotificationService : INotificationService
    {
        private readonly ISmsService _smsService;

        public NotificationService(ISmsService smsService)
        {
            _smsService = smsService;
        }


        public async Task<bool> SendAcceptWidrwalRequestNotifications(SendNotificationVM model)
        {
            await _smsService.AcceptWidthrawlRequestByAdmin(model.Text, model.UserName);
            return true;
        }



        public async Task<bool> SendRejectWidrwalRequestNotifications(SendNotificationVM model)
        {
            await _smsService.AcceptWidthrawlRequestByAdmin(model.Text, model.UserName);
            return true;
        }


    }


}

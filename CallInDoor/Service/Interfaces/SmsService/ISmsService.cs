using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.SmsService
{
    public interface ISmsService
    {
        Task<(bool isSuccess, string error)> SendMessage(string To, string data, string pattern);
        Task<(bool isSuccess, string error)> RegistrerCode(string code, string phoneNumber);
        Task<(bool isSuccess, string error)> RecoveryPassword(string code, string phoneNumber);
        Task<(bool isSuccess, string error)> ConfirmServiceByAdmin(string code, string phoneNumber);
        Task<(bool isSuccess, string error)> AcceptWidthrawlRequestByAdmin(string code, string phoneNumber);
        Task<(bool isSuccess, string error)> RejectWidthrawlRequestByAdmin(string code, string phoneNumber);


    }
}

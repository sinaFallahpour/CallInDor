using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.RequestService
{
    public class SendChatToChatServiceDTO
    {


        public string Text { get; set; }

        public IFormFile File { get; set; }
        public bool IsFile { get; set; }

        public IFormFile Voice { get; set; }

        public bool IsVoice { get; set; }

        #region  Relation 

        public int? ServiceRequestId { get; set; }

        #endregion


    }
}

using EncryptedBoxAPI.Models.EMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncryptedBoxAPI.Services
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}

using Storage.Models;

namespace Storage.Services.ServiceEmail
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}

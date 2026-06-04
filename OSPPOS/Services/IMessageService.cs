using OSPPOS.Models;

namespace DMX.Services
{
    public interface IMessageService
    {
        List<Message> GetMessages(string Receiver, bool bIsGetOnlyUnread);
    }
}

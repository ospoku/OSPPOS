using DMX.Services;
using OSPPOS.Data;
using OSPPOS.Models;

namespace OSPPOS.Services
{
    public class MessageService(XContext context) : IMessageService
    {

        public readonly XContext xct = context;
        List<Message> userMessages = new();

        public List<Message> GetMessages(string Receiver, bool bIsGetOnlyUnread, XContext xct)
        {

            userMessages = (from m in xct.Messages where m.Reciever == Receiver & m.IsRead == 0 select m).ToList();


            return userMessages;
        }

        public List<Message> GetMessages(string Receiver, bool bIsGetOnlyUnread)
        {
            throw new NotImplementedException();
        }
    }
}

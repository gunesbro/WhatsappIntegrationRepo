using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WhatsappIntegration.DAL.Abstract;
using WhatsappIntegration.DAL.Context;
using WhatsappIntegration.Entity.Concrete;
using WhatsappIntegration.Utility;

namespace WhatsappIntegration.DAL.Concrete.EFCore
{
    public class EfChatMessagesRepository : GenericRepository<ChatMessages>, IChatMessagesRepository
    {
        public EfChatMessagesRepository(IdentityContext context) : base(context)
        {
        }

        public IdentityContext _context
        {
            get { return context as IdentityContext; }
        }
        public int GetUnreadMessageCount(int chatId)
        {
            int unreadMessageCount = _context.ChatMessages.Where(x => x.IsItRead == false && x.MessageDirection == Enums.ChatMessageFromClient && x.ChatId == chatId).Count();

            return unreadMessageCount;
        }
        public bool SetUnreadToRead(int chatId)
        {
            string query = "UPDATE ChatMessages SET IsItRead = 0 WHERE MessageDirection ="+ Enums.ChatMessageFromClient +" AND IsItRead = 0 AND ChatId = " + chatId;
            RawSqlQuery(query);
            return true;
        }
    }
}

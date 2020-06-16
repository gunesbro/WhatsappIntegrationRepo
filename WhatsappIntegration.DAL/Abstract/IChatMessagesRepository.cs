using System;
using System.Collections.Generic;
using System.Text;
using WhatsappIntegration.Entity.Concrete;

namespace WhatsappIntegration.DAL.Abstract
{
    public interface IChatMessagesRepository:IGenericRepository<ChatMessages>
    {
        int GetUnreadMessageCount(int chatId);
        bool SetUnreadToRead(int chatId);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsappIntegration.DAL.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IChatRepository Chat { get; }
        IChatMessagesRepository ChatMessages { get; }
        IChatTypesRepository ChatTypes { get; }
        ICompanyRepository Company { get; }
        IDirectoryRepository Directory { get; }
        ISmartReplyRepository SmartReply { get; }
        int SaveChanges();

    }
}

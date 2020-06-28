using System;
using System.Collections.Generic;
using System.Text;
using WhatsappIntegration.DAL.Abstract;
using WhatsappIntegration.DAL.Context;

namespace WhatsappIntegration.DAL.Concrete.EFCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityContext dbContext;
        public UnitOfWork(IdentityContext _dbContext)
        {
            dbContext = _dbContext ?? throw new ArgumentNullException("dbcontext can not be null");
        }
        private IChatRepository _chat;
        private IChatMessagesRepository _chatMessages;
        private IChatTypesRepository _chatTypes;
        private ICompanyRepository _company;
        private IDirectoryRepository _directory;
        private ISmartReplyRepository _smartReply;
        private IVChatListRepository _vChatList;
        public IChatRepository Chat
        {
            get
            {
                return _chat ?? (_chat = new EfChatRepository(dbContext));
            }
        }

        public IChatMessagesRepository ChatMessages
        {
            get
            {
                return _chatMessages ?? (_chatMessages = new EfChatMessagesRepository(dbContext));
            }
        }
        public IChatTypesRepository ChatTypes
        {
            get
            {
                return _chatTypes ?? (_chatTypes = new EfChatTypesRepository(dbContext));
            }
        }
        public ICompanyRepository Company
        {
            get
            {
                return _company ?? (_company = new EfCompanyRepository(dbContext));
            }
        }

        public IDirectoryRepository Directory
        {
            get
            {
                return _directory ?? (_directory = new EfDirectoryRepository(dbContext));
            }
        }

        public ISmartReplyRepository SmartReply
        {
            get
            {
                return _smartReply ?? (_smartReply = new EfSmartReplyRepository(dbContext));
            }
        }
        public IVChatListRepository VChatList
        {
            get
            {
                return _vChatList ?? (_vChatList = new EfVChatListRepository(dbContext));
            }
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }

        public int SaveChanges()
        {
            try
            {
                return dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

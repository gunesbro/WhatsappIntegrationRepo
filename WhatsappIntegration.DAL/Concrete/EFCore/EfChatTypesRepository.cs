using System;
using System.Collections.Generic;
using System.Text;
using WhatsappIntegration.DAL.Abstract;
using WhatsappIntegration.DAL.Context;
using WhatsappIntegration.Entity.Concrete;

namespace WhatsappIntegration.DAL.Concrete.EFCore
{
    public class EfChatTypesRepository : GenericRepository<ChatTypes>,IChatTypesRepository
    {
        public EfChatTypesRepository(IdentityContext context) : base(context)
        {
        }
        public IdentityContext IdentityContext
        {
            get { return context as IdentityContext; }
        }

    }
}

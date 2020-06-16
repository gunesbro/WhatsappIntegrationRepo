using System;
using System.Collections.Generic;
using System.Text;
using WhatsappIntegration.DAL.Abstract;
using WhatsappIntegration.DAL.Context;
using WhatsappIntegration.Entity.Concrete;

namespace WhatsappIntegration.DAL.Concrete.EFCore
{
    public class EfSmartReplyRepository:GenericRepository<SmartReplies>,ISmartReplyRepository
    {
        public EfSmartReplyRepository(IdentityContext context) : base(context)
        {
        }

        public IdentityContext IdentityContext
        {
            get { return context as IdentityContext; }
        }
    }
}

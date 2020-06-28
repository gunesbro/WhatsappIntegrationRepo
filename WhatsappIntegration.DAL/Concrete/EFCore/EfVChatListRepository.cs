using System;
using System.Collections.Generic;
using System.Text;
using WhatsappIntegration.DAL.Abstract;
using WhatsappIntegration.DAL.Context;
using WhatsappIntegration.Entity.Concrete;

namespace WhatsappIntegration.DAL.Concrete.EFCore
{
    public class EfVChatListRepository : GenericRepository<VChatList>, IVChatListRepository
    {
        public EfVChatListRepository(IdentityContext context):base(context)
        {

        }
    }
}

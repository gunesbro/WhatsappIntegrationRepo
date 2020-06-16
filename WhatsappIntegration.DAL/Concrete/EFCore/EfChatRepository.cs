using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhatsappIntegration.DAL.Abstract;
using WhatsappIntegration.DAL.Context;
using WhatsappIntegration.Entity.Concrete;

namespace WhatsappIntegration.DAL.Concrete.EFCore
{
    public class EfChatRepository : GenericRepository<Chat>, IChatRepository
    {
        public EfChatRepository(IdentityContext context):base(context)
        {
        }

        public IdentityContext IdentityContext
        {
            get { return context as IdentityContext; }
        }

        public List<Chat> GetAllChats()
        {
            try
            {
                var result = GetAll().Where(f => f.IsDeleted == false).ToList();
                return result;

            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}

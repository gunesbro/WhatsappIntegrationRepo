using System;
using System.Collections.Generic;
using System.Text;
using WhatsappIntegration.Entity.Concrete;

namespace WhatsappIntegration.DAL.Abstract
{
    public interface IChatRepository : IGenericRepository<Chat>
    {
        /// <summary>
        /// Returns 'IsDeleted == false' records
        /// </summary>
        /// <returns></returns>
        List<Chat> GetAllChats();
    }
}

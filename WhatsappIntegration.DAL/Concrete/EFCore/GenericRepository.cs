using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WhatsappIntegration.DAL.Abstract;
using WhatsappIntegration.DAL.Context;

namespace WhatsappIntegration.DAL.Concrete.EFCore
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly IdentityContext context;
        public GenericRepository(IdentityContext _context)
        {
            context = _context;
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

       
        public IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Insert(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void RawSqlQuery(string query)
        {
            try
            {
                context.Database.ExecuteSqlRaw(query);
            }
            catch (Exception ex)
            {
            }
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}

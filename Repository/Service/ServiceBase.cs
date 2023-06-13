using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Service
{
    public class ServiceBase<T> where T : class
    {
        private readonly API_TEST_DBContext _context;
        private readonly DbSet<T> _dbSet;

        public ServiceBase()
        {
            _context = new API_TEST_DBContext();
            _dbSet = _context.Set<T>();
        }
        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(String id)
        {
            return _dbSet.Find(id);
        }
       
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public bool DeleteById(string id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
                return true;
            }else return false;
        }
        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
        public List<T> Search(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }
        public List<T> SearchByPartOfValue(Func<T, string> keywordSelector, string keyword)
        {
            string lowercaseKeyword = keyword.ToLower();
            List<T> entities = _dbSet.ToList(); // Retrieve all entities from the database

            return entities.Where(e => keywordSelector(e).ToLower().Contains(lowercaseKeyword)).ToList();
        }
    }
}

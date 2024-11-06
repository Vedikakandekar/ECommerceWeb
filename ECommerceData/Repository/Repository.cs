﻿using ECommerce.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        internal DbSet<T> Set;
        public Repository(ApplicationDbContext db)
        {
            _db = db;

            this.Set = _db.Set<T>();  
            // here _db.Category  is equivalent to dbset (Set in this case) 
            // as IRepository and Repository are generic , we dont know  which class will request Repository  at run time so , 
            //we are setting dbset to _db.Set<T> , i.e in case of category class, Set will contain _db.Category
            _db.Product.Include(u => u.Category).Include( u => u.CategoryId);
        }
        public void Add(T item)
        {
            Set.Add(item);
        }
        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filer, string? includeProperties)
        {
            IQueryable<T> query = Set;
            query = query.Where(filer);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault()!;
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? filer , string? includeProperties)
        {
            IQueryable<T> query = Set;
            if(filer!=null)
            {
                query = query.Where(filer);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T item)
        {
            Set.Remove(item);
        }

        public void RemoveRane(IEnumerable<T> items)
        {
            Set.RemoveRange(items);
        }
    }
}

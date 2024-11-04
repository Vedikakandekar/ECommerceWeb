using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {

        //T=Category

        IEnumerable<T> GetAll(string? includeProperties = null);

        T Get(Expression<Func<T, bool>> filer, string? includeProperties =  null);

        void Add(T item);

        void Remove(T item);

        void RemoveRane(IEnumerable<T> items);
    }
}

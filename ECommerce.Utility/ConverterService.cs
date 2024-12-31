using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Utility
{
    public class ConverterService
    {
        public T Convert<T>(string value)
        {
            try
            {
                return (T)System.Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                throw new InvalidCastException($"Cannot convert '{value}' to type {typeof(T).Name}");
            }
        }
    }
}

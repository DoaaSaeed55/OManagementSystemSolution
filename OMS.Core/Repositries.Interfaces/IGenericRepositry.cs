using OMS.Core.Entities;
using OMS.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Repositries.Interfaces
{
    public interface IGenericRepositry<T> where T : BaseEntity
    {

        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int id);


        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T?> GetWithSpecAsync(ISpecification<T> spec);


        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
    }
}

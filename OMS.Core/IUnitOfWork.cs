using OMS.Core.Entities;
using OMS.Core.Repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IGenericRepositry<TEntity> Repositry<TEntity>() where TEntity : BaseEntity;
        Task<int> CompleteAsync();
    }
}

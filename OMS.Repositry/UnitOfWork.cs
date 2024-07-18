using OMS.Core;
using OMS.Core.Entities;
using OMS.Core.Repositries.Interfaces;
using OMS.Repositry.Data;
using OMS.Repositry.Repostries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Repositry
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OMSDbContext _Context;
        private Hashtable _repositry;
        public UnitOfWork(OMSDbContext Context) 
        {
            _Context = Context;
            _repositry = new Hashtable();
        }
        public async Task<int> CompleteAsync()=> await _Context.SaveChangesAsync();
       

        public ValueTask DisposeAsync()=>_Context.DisposeAsync();
        

        public IGenericRepositry<TEntity> Repositry<TEntity>() where TEntity : BaseEntity
        {
            var type=typeof(TEntity).Name;
            if (_repositry.ContainsKey(type))
            {
                var repositry = new GenericRepositry<TEntity>(_Context);
                _repositry.Add(type, _repositry);
            }
            
            return _repositry[type] as IGenericRepositry<TEntity>;
        }
    }
}

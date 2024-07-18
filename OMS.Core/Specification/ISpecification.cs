using OMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Specification
{
    public interface ISpecification<T> where T :BaseEntity
    {
        public Expression<Func<T,bool>> Criteira { get; set; }
        public List<Expression<Func<T,object>>> Include { get; set; }



    }
}

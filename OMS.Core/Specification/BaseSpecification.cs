using OMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteira { get; set; } = null;
        public List<Expression<Func<T, object>>> Include { get; set ; }=new List<Expression<Func<T, object>>>();

        public BaseSpecification() 
        { 
            //Include=new List<Expression<Func<T, object>>>();
            //Criteira = null;
        }


        public BaseSpecification(Expression<Func<T,bool>> CriteriaExpression)
        {
            //Include = new List<Expression<Func<T, object>>>();
            Criteira = CriteriaExpression;
        }

    }
}

using Microsoft.EntityFrameworkCore;
using OMS.Core.Entities;
using OMS.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Repositry.Specification
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecification<TEntity> spec)
        {
            var query= inputQuery;

            if(spec.Criteira is not null)
            {
                query = query.Where(spec.Criteira);
            }

            spec.Include.Aggregate(query, (CurrentQuery, includeExpression) => CurrentQuery.Include(includeExpression));
            return query;
        }
    }
}

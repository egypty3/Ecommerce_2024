using Core.Enums;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria
        {
            get;
            private set;
        }
        public List<Expression<Func<T, object>>> Includes
        {
            get;
        } = new List<Expression<Func<T, object>>>();

        

        public void AddInclude(
            Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        public void SetCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, object>> OrderBy { get; private set; } = null;

        public OrderBy OrderByDirection { get; private set; } = Core.Enums.OrderBy.Ascending;

        public void ApplyOrderBy(Expression<Func<T, object>> orderByExpression, OrderBy direction)
        {
            OrderBy = orderByExpression;
            OrderByDirection = direction;
        }
    }
}
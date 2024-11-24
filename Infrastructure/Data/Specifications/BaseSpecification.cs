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
        public BaseSpecification()
        {
            Criteria = new List<Expression<Func<T, bool>>>();
            Includes = new List<Expression<Func<T, object>>>();
        }

        public List<Expression<Func<T, bool>>> Criteria 
        {
            get;
        }
        public List<Expression<Func<T, object>>> Includes
        {
            get;
        } 



        public void AddInclude(
            Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }


        public Expression<Func<T, object>> OrderBy { get; private set; } = null;

        public OrderBy OrderByDirection { get; private set; } = Core.Enums.OrderBy.Ascending;

        public int Take { get; private set; } = -1; // No paging by default

        public int Skip { get; private set; } = 0; // Start from the record by default

       public bool IsPagingEnabled { get; private set; } = false;

        protected void AddCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria.Add(criteria);
        }

        public void ApplyOrderBy(Expression<Func<T, object>> orderByExpression, OrderBy direction)
        {
            OrderBy = orderByExpression;
            OrderByDirection = direction;
        }

        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        List<Expression<Func<T, bool>>> Criteria { get; }

        // p => p.price > 20
        // p.brand == "Addidas"

        List<Expression<Func<T, object>>> Includes { get; }

        Expression<Func<T, object>> OrderBy { get; }

        OrderBy OrderByDirection { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}

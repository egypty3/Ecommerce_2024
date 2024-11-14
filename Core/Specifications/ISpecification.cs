using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }

        // p => p.price > 20
        // p.brand == "Addidas"

        List<Expression<Func<T, object>>> Includes { get; }
    }
}

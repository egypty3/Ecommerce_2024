using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Specifications
{
    public class EmptySpecification<T> : BaseSpecification<T>
    {
        public EmptySpecification()
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Rules
{
    interface IRule
    {
        List<int>  Apply(List<int> input);
    }
}

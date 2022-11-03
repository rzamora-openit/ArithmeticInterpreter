using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Frameworks.Automata
{
    abstract class Token
    {
        public abstract bool Have(string token);

    }
}

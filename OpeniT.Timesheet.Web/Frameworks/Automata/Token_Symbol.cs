using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Frameworks.Automata
{
    class Token_Symbol : Token
    {
        public string symbol;
        public Token_Symbol(string symbol)
        {
            this.symbol = symbol;
        }

        public override bool Have(string token)
        {
            if (symbol.CompareTo(token) == 0)
                return true;
            else
                return false;
        }
    }
}

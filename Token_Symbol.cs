using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    class Token_Symbol : Token
    {
        public String symbol;
        public Token_Symbol(String symbol)
        {
            this.symbol = symbol;
        }

        public override bool have(String token)
        {
            if (symbol.CompareTo(token) == 0)
                return true;
            else
                return false;
        }
    }
}

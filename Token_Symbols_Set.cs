using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    class Token_Symbols_Set : Token
    {

        public List<String> symbols;

        public Token_Symbols_Set(String[] symbols)
        {
            this.symbols = new List<String>();
            foreach (String symbol in symbols)
            {
                this.symbols.Add(symbol);
            }
        }

        public override Boolean have(String token)
        {
            foreach (String symbol in this.symbols)
            {
                if (symbol.CompareTo(token) == 0) return true;
            }
            return false;
        }
    }
}

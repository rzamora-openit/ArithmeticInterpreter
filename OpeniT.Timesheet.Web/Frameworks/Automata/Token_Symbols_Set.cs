using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Frameworks.Automata
{
    class Token_Symbols_Set : Token
    {

        public List<string> symbols;

        public Token_Symbols_Set(string[] symbols)
        {
            this.symbols = new List<string>();
            foreach (string symbol in symbols)
            {
                this.symbols.Add(symbol);
            }
        }

        public override bool Have(string token)
        {
            foreach (string symbol in symbols)
            {
                if (symbol.CompareTo(token) == 0) return true;
            }
            return false;
        }
    }
}

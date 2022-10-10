using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    class Alphabet : List<Token>
    {
        public Alphabet()
        {

        }

        public Alphabet(String[] symbols)
        {
            foreach (String symbol in symbols)
            {
                Add(new Token_Symbol(symbol));
            }
        }

        public Alphabet(List<String> symbols)
        {
            foreach (String symbol in symbols)
            {
                Add(new Token_Symbol(symbol));
            }
        }

        public Token getToken(String symbol)
        {
            foreach (Token token in this)
            {
                if (token.have(symbol))
                    return token;
            }
            throw new Exception($"Symbol {symbol} not found in Alphabet");
        }
    }
}
